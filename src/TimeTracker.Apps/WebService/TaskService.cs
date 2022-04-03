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
    internal class TaskService
    {
        public static async Task<ObservableCollection<TaskItem>> GetAllTask(long projectId)

        {
            HttpClient client = new HttpClient();

            var resquest = new HttpRequestMessage()
            {
                RequestUri = new Uri(Dtos.Urls.HOST + "/api/v1/projects/"+projectId.ToString()+ "/tasks"),
                Method = HttpMethod.Get
            };

            resquest.Headers.Add("Authorization", "Bearer " + Preferences.Get("access_token", null));

            HttpResponseMessage response = await client.SendAsync(resquest);

            if (response.IsSuccessStatusCode)
            {
                JObject data = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                if (data.GetValue("is_success").ToString() == "True")
                {
                    ObservableCollection<TaskItem> jsonData = JsonConvert.DeserializeObject<ObservableCollection<TaskItem>>(data.GetValue("data").ToString());
                    return jsonData;
                }
            }
            else
            {
                JObject data = JObject.Parse(response.Content.ReadAsStringAsync().Result);

                if (data.GetValue("error_code").ToString() == "Unauthorized")
                {
                    await AuthentificationService.Refresh();
                    Console.WriteLine("GetAllTask problème de Token");
                }
                else
                {
                    Console.WriteLine("GetAllTask ne fonctionne pas");
                }
            }

            return null;
        }

        public static async Task<TaskItem> GetTaskById(long projectId, long tacheId)
        {
            ObservableCollection<TaskItem> listeTache= await GetAllTask(projectId);
            if (listeTache != null)
            {
                for (int i = 0; i < listeTache.Count; i++)
                {
                    if (listeTache[i].Id == tacheId)
                    {
                        Console.WriteLine("GetTaskByID à fonctionné");
                        return listeTache[i];
                    }
                }
            }
            return null;
        }

        public static async Task<TaskItem> AddTask(String nom, long projectId)
        {
            HttpClient client = new HttpClient();

            TaskItem projet = new TaskItem()
            {
                Name = nom,
            };

            var body = JsonConvert.SerializeObject(projet);

            HttpRequestMessage resquest = new HttpRequestMessage()
            {
                RequestUri = new Uri(Dtos.Urls.HOST + "/api/v1/projects/"+projectId.ToString()+ "/tasks"),
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
                    TaskItem jsonData = JsonConvert.DeserializeObject<TaskItem>(data.GetValue("data").ToString());
                    return jsonData;
                }
            }
            else
            {
                JObject data = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                if (data.GetValue("error_code").ToString() == "Unauthorized")
                {
                    await AuthentificationService.Refresh();
                    Console.WriteLine("AddTask problème de Token");
                }
                else
                {
                    Console.WriteLine("AddTask ne fonctionne pas");
                }
            }
            return null;
        }

        public static async Task<TaskItem> UpdateTask(String nom, long taskId, long projectId)
        {
            HttpClient client = new HttpClient();

            TaskItem projet = new TaskItem()
            {
                Name = nom,
            };

            var body = JsonConvert.SerializeObject(projet);

            HttpRequestMessage resquest = new HttpRequestMessage()
            {
                RequestUri = new Uri(Dtos.Urls.HOST + "/api/v1/projects/" + projectId.ToString() + "/tasks"+taskId.ToString()),
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
                    TaskItem jsonData = JsonConvert.DeserializeObject<TaskItem>(data.GetValue("data").ToString());
                    return jsonData;
                }
            }
            else
            {
                JObject data = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                if (data.GetValue("error_code").ToString() == "Unauthorized")
                {
                    await AuthentificationService.Refresh();
                    Console.WriteLine("UpdateTask problème de Token");
                }
                else
                {
                    Console.WriteLine("UpdateTask ne fonctionne pas");
                }
            }
            return null;
        }

        public static async Task<bool> DeleteTask(long projectId, long taskId)
        {
            HttpClient client = new HttpClient();

            HttpRequestMessage resquest = new HttpRequestMessage()
            {
                RequestUri = new Uri(Dtos.Urls.HOST + "/api/v1/projects/" + projectId.ToString() + "/tasks/" + taskId.ToString()),
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
                    Console.WriteLine("DeleteTask problème de Token");
                }
                else
                {
                    Console.WriteLine("DeleteTask ne fonctionne pas");
                }
            }
            return false;
        }
    }
}
