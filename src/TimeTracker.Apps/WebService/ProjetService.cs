using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.Dtos.Projects;
using Xamarin.Essentials;

namespace TimeTracker.Apps.WebService
{
    internal class ProjetService
    {
        public static async Task<ObservableCollection<ProjectItem>> GetAllProject()
        {
            HttpClient client = new HttpClient();

            HttpRequestMessage resquest = new HttpRequestMessage()
            {
                RequestUri = new Uri(Dtos.Urls.HOST + "/" + Dtos.Urls.LIST_PROJECTS),
                Method = HttpMethod.Get,
            };

            resquest.Headers.Add("Authorization", "Bearer " + Preferences.Get("access_token", null));


            HttpResponseMessage response = await client.SendAsync(resquest);

            if (response.IsSuccessStatusCode)
            {
                JObject data = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                if (data.GetValue("is_success").ToString() == "True")
                {
                    ObservableCollection<ProjectItem> jsonData = JsonConvert.DeserializeObject<ObservableCollection<ProjectItem>>(data.GetValue("data").ToString());
                    return jsonData;
                }
            }
            else
            {
                JObject data = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                if (data.GetValue("error_code").ToString() == "Unauthorized")
                {
                    await AuthentificationService.Refresh();
                    Console.WriteLine("GetAllProject problème de Token");
                }
                else
                {
                    Console.WriteLine("GetAllProject ne fonctionne pas");
                }

            }
            return null;
        }

        public static async Task<ProjectItem> GetProjetById(long idProjet)
        {
            ObservableCollection<ProjectItem> listeProjet = await GetAllProject();
            if (listeProjet!=null){
                for(int i=0; i<listeProjet.Count; i++)
                {
                    if (listeProjet[i].Id == idProjet)
                    {
                        return listeProjet[i];
                    }
                }
            }
            return null;            
        }

        public static async Task<ProjectItem> AddProject(String nom, String description)
        {
            HttpClient client = new HttpClient();

            ProjectItem projet = new ProjectItem()
            {
                Name = nom,
                Description = description
            };

            var body = JsonConvert.SerializeObject(projet);

            HttpRequestMessage resquest = new HttpRequestMessage()
            {
                RequestUri = new Uri(Dtos.Urls.HOST + "/" + Dtos.Urls.ADD_PROJECT),
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
                JObject data = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                if (data.GetValue("error_code").ToString() == "Unauthorized")
                {
                    await AuthentificationService.Refresh();
                    Console.WriteLine("AddProject problème de Token");
                }
                else
                {
                    Console.WriteLine("AddProject ne fonctionne pas");
                }

            }
            return null;
        }

        public static async Task<ProjectItem> UpdateProject(String nom, String description, long projectId)
        {
            HttpClient client = new HttpClient();

            ProjectItem projet = new ProjectItem()
            {
                Name = nom,
                Description = description
            };

            var body = JsonConvert.SerializeObject(projet);

            HttpRequestMessage resquest = new HttpRequestMessage()
            {
                RequestUri = new Uri(Dtos.Urls.HOST + "/api/v1/projects/"+projectId.ToString()),
                Method = HttpMethod.Put,
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
                JObject data = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                if (data.GetValue("error_code").ToString() == "Unauthorized")
                {
                    await AuthentificationService.Refresh();
                    Console.WriteLine("UpdateProject problème de Token");
                }
                else
                {
                    Console.WriteLine("UpdateProject ne fonctionne pas");
                }
            }
            return null;
        }

        public static async Task<bool> DeleteProject(long projectId)
        {
            HttpClient client = new HttpClient();

            HttpRequestMessage resquest = new HttpRequestMessage()
            {
                RequestUri = new Uri(Dtos.Urls.HOST + "/api/v1/projects/"+projectId.ToString()),
                Method = HttpMethod.Delete
            };

            resquest.Headers.Add("Authorization", "Bearer " + Preferences.Get("access_token", null));
            HttpResponseMessage response = await client.SendAsync(resquest);

            if (response.IsSuccessStatusCode)
            {
                JObject data = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                if (data.GetValue("is_success").ToString() == "True")
                {
                    return true;
                }
            }
            else
            {
                JObject data = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                if (data.GetValue("error_code").ToString() == "Unauthorized")
                {
                    await AuthentificationService.Refresh();
                    Console.WriteLine("DeleteProject problème de Token");
                }
                else
                {
                    Console.WriteLine("DeleteProject ne fonctionne pas");
                }
            }
            return false;
        }
    }
}