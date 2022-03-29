using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.Dtos.Accounts;
using TimeTracker.Dtos.Authentications.Credentials;
using Xamarin.Essentials;

namespace TimeTracker.Apps.WebService
{
    internal class AccountService
    {
        public static async Task<UserProfileResponse> GetProfil()
        {
            HttpClient client = new HttpClient();

            var resquest = new HttpRequestMessage()
            {
                RequestUri = new Uri(Dtos.Urls.HOST + "/" + Dtos.Urls.USER_PROFILE),
                Method = HttpMethod.Get
            };

            resquest.Headers.Add("Authorization", "Bearer " + Preferences.Get("access_token", null));

            HttpResponseMessage response = await client.SendAsync(resquest);

            if (response.IsSuccessStatusCode)
            {
                JObject data = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                if (data.GetValue("is_success").ToString() == "True")
                {
                    UserProfileResponse jsonData = JsonConvert.DeserializeObject<UserProfileResponse>(data.GetValue("data").ToString());
                    return jsonData;
                }
            }
            else
            {
                JObject data = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                if (data.GetValue("error_code").ToString() == "Unauthorized")
                {
                    await AuthentificationService.Refresh();
                    Console.WriteLine("GetProfil problème de Token");
                }
                else
                {
                    Console.WriteLine("GetProfil ne fonctionne pas");
                }
            }
            return null;
        }

        public static async Task<UserProfileResponse> UpdateProfil(String email, String firstName, String lastName)
        {
            HttpClient client = new HttpClient();

            /*UserProfileResponse profil = new UserProfileResponse()
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName
            };*/

            UserProfileResponse profil = new UserProfileResponse()
            {
                Email = "imis@gmail.com",
                FirstName = "Julien",
                LastName = "Chieze"
            };

            var body = JsonConvert.SerializeObject(profil);

            var resquest = new HttpRequestMessage()
            {
                RequestUri = new Uri(Dtos.Urls.HOST + "/" + Dtos.Urls.SET_USER_PROFILE),
                Method = new HttpMethod("PATCH"),
                Content = new StringContent(body, Encoding.UTF8, "application/json")
            };


            resquest.Headers.Add("Authorization", "Bearer " + Preferences.Get("access_token", null));

            HttpResponseMessage response = await client.SendAsync(resquest);

            if (response.IsSuccessStatusCode)
            {
                JObject data = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                if (data.GetValue("is_success").ToString() == "True")
                {
                    UserProfileResponse jsonData = JsonConvert.DeserializeObject<UserProfileResponse>(data.GetValue("data").ToString());
                    return jsonData;
                }
            }
            else
            {
                JObject data = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                if (data.GetValue("error_code").ToString() == "Unauthorized")
                {
                    await AuthentificationService.Refresh();
                    Console.WriteLine("UpdateProfil problème de Token");
                }
                else
                {
                    Console.WriteLine("UpdateProfil ne fonctionne pas");
                }
            }
            return null;
        }

        public static async Task<Boolean> SetPassword(String old_password, String new_password, String lastName)
        {
            HttpClient client = new HttpClient();

            /*UserProfileResponse profil = new UserProfileResponse()
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName
            };*/

            SetPasswordRequest profil = new SetPasswordRequest()
            {
                OldPassword = old_password,
                NewPassword = new_password
            };

            var body = JsonConvert.SerializeObject(profil);

            var resquest = new HttpRequestMessage()
            {
                RequestUri = new Uri(Dtos.Urls.HOST + "/" + Dtos.Urls.SET_PASSWORD),
                Method = new HttpMethod("PATCH"),
                Content = new StringContent(body, Encoding.UTF8, "application/json")
            };


            resquest.Headers.Add("Authorization", "Bearer " + Preferences.Get("access_token", null));

            HttpResponseMessage response = await client.SendAsync(resquest);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                JObject data = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                if (data.GetValue("error_code").ToString() == "Unauthorized")
                {
                    await AuthentificationService.Refresh();
                    Console.WriteLine("setPassword problème de Token");
                }
                else
                {
                    Console.WriteLine("setPassword ne fonctionne pas");
                }
            }
            return false;
        }



    }
}
