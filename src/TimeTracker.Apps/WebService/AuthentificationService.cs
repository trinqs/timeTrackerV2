﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Newtonsoft.Json.Linq;
using TimeTracker.Dtos.Authentications.Credentials;
using TimeTracker.Dtos.Authentications;
using TimeTracker.Dtos.Accounts;

namespace TimeTracker.Apps.WebService
{
    internal class AuthentificationService
    {

        private const string API_URL = "https://timetracker.julienmialon.ovh/api/v1";
        private const string CLIENT_ID = "MOBILE";
        private const string CLIENT_SECRET = "COURS";
        private const string token_type = "Bearer";



        public AuthentificationService() { }

        public static async Task Inscription(String email, string firstName, string lastName, string password)
        {
            HttpClient client = new HttpClient();
            /*CreateUserRequest inscription = new CreateUserRequest()
            {
                ClientId = CLIENT_ID,
                ClientSecret = CLIENT_SECRET
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Password = password,

            };*/

            CreateUserRequest inscription = new CreateUserRequest()
            {
                ClientId = CLIENT_ID,
                ClientSecret = CLIENT_SECRET,
                Email = "imis2@gmail.com",
                FirstName = "test",
                LastName = "test",
                Password = "test",

            };

            var body = JsonConvert.SerializeObject(inscription);

            var resquest = new HttpRequestMessage()
            {
                RequestUri = new Uri(Dtos.Urls.HOST + Dtos.Urls.CREATE_USER),
                Method = HttpMethod.Post,
                Content = new StringContent(body, Encoding.UTF8, "application/json")
            };

            HttpResponseMessage response = await client.SendAsync(resquest);

            if (response.IsSuccessStatusCode)
            {
                JObject data = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                if (data.GetValue("is_success").ToString() == "True")
                {
                    LoginResponse jsonData = JsonConvert.DeserializeObject<LoginResponse>(data.GetValue("data").ToString());

                    Preferences.Set("access_token", jsonData.AccessToken);
                    Preferences.Set("refresh_token", jsonData.RefreshToken);
                    Preferences.Set("token_type", jsonData.TokenType);
                }
            }
        }

        public static async Task Connexion(String loginRequete, String passwordRequete)
        {
            Console.WriteLine("JE suis LA");
            HttpClient client = new HttpClient();
            LoginWithCredentialsRequest login = new LoginWithCredentialsRequest()
            {
                ClientId = CLIENT_ID,
                ClientSecret = CLIENT_SECRET,
                Password = "test",
                Login = "imis@gmail.com"
            };

            var body = JsonConvert.SerializeObject(login);

            var resquest = new HttpRequestMessage()
            {
                RequestUri = new Uri(Dtos.Urls.HOST +"/"+ Dtos.Urls.LOGIN),
                Method = HttpMethod.Post,
                Content = new StringContent(body, Encoding.UTF8, "application/json")
            };

            HttpResponseMessage response = await client.SendAsync(resquest);

            if (response.IsSuccessStatusCode)
            {
                JObject data = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                if (data.GetValue("is_success").ToString() == "True")
                {
                    LoginResponse jsonData = JsonConvert.DeserializeObject<LoginResponse>(data.GetValue("data").ToString());

                    Console.WriteLine("acess Token : " + jsonData.AccessToken);

                    Preferences.Set("access_token", jsonData.AccessToken);
                    Preferences.Set("refresh_token", jsonData.RefreshToken);
                    Preferences.Set("token_type", jsonData.TokenType);
                }
            }
        }

        public static async Task Refresh()
        {
            //var request = RequestInscription(Method.Post);
            HttpClient client = new HttpClient();
            RefreshRequest refresh = new RefreshRequest()
            {
                RefreshToken = Preferences.Get("refresh_token", ""),
                ClientId = CLIENT_ID,
                ClientSecret = CLIENT_SECRET
            };

            var body = JsonConvert.SerializeObject(refresh);

            var resquest = new HttpRequestMessage()
            {
                RequestUri = new Uri(Dtos.Urls.HOST + "/login"),
                Method = HttpMethod.Post,
                Content = new StringContent(body, Encoding.UTF8, "application/json")
            };

            HttpResponseMessage response = await client.SendAsync(resquest);

            if (response.IsSuccessStatusCode)
            {
                JObject data = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                if (data.GetValue("is_success").ToString() == "True")
                {
                    LoginResponse jsonData = JsonConvert.DeserializeObject<LoginResponse>(data.GetValue("data").ToString());

                    var test = jsonData.GetType().ToString();

                    Preferences.Set("access_token", jsonData.AccessToken);
                    Preferences.Set("refresh_token", jsonData.RefreshToken);
                    Preferences.Set("token_type", jsonData.TokenType);
                }
            }
        }
    }
}
