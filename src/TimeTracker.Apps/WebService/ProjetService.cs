using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.Dtos.Projects;
using Xamarin.Essentials;

namespace TimeTracker.Apps.WebService
{
    internal class ProjetService
    {
        public static async Task<List<ProjectItem>> GetAllProject()
        {
            HttpClient client = new HttpClient();

            HttpRequestMessage resquest = new HttpRequestMessage()
            {
                RequestUri = new Uri(Dtos.Urls.HOST +"/"+ Dtos.Urls.LIST_PROJECTS),
                Method = HttpMethod.Get,
            };

            resquest.Headers.Add("Authorization", "Bearer " + Preferences.Get("access_token", null));


            HttpResponseMessage response = await client.SendAsync(resquest);

            if (response.IsSuccessStatusCode)
            {


                JObject data = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                if (data.GetValue("is_success").ToString() == "True")
                {
                    List<ProjectItem> jsonData = JsonConvert.DeserializeObject<List<ProjectItem>>(data.GetValue("data").ToString());
                    
                    return jsonData;
                }
            }
            else
            {
                Console.WriteLine("GetAllProject ne fonctionne pas");
                
            }
            return null;
        }

        public static async Task<ProjectItem> AddProject(String nom, String description)
        {
            HttpClient client = new HttpClient();

            /*ProjectItem projet = new ProjectItem()
            {
                Name = nom,
                Description = description
            };*/

            ProjectItem projet = new ProjectItem() {
                Name = "projet 1",
                Description = "C'est la descripton"
            };

            var body = JsonConvert.SerializeObject(projet);

            HttpRequestMessage resquest = new HttpRequestMessage()
            {
                RequestUri = new Uri(Dtos.Urls.HOST + "/" + Dtos.Urls.LIST_PROJECTS),
                Method = HttpMethod.Post,
                Content = new StringContent(body, Encoding.UTF8, "application/json")

            };

            resquest.Headers.Add("Authorization", "Bearer " + Preferences.Get("access_token", null));


            HttpResponseMessage response = await client.SendAsync(resquest);

            if (response.IsSuccessStatusCode)
            {
                JObject data = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                if (data.GetValue("is_success").ToString() == "True")
                {
                    ProjectItem jsonData = JsonConvert.DeserializeObject<ProjectItem>(data.GetValue("data").ToString());
                    return jsonData;
                }
            }
            else
            {
                Console.WriteLine("GetAllProject ne fonctionne pas");

            }
            return null;
        }

    }
}
