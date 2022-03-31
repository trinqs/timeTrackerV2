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
    internal class TimeService
    {
        public static async Task<TimeItem> AddProfil(int projectId, int taskId, DateTime startTime, DateTime endTime)
        {
            HttpClient client = new HttpClient();

            TimeItem time = new TimeItem()
            {
                StartTime = startTime,
                EndTime = endTime,
            };

            var body = JsonConvert.SerializeObject(time);

            var resquest = new HttpRequestMessage()
            {
                RequestUri = new Uri(Dtos.Urls.HOST + "/api/v1/projects/" + projectId.ToString() + "/tasks" + taskId.ToString()+"/times"),
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
                    TimeItem jsonData = JsonConvert.DeserializeObject<TimeItem>(data.GetValue("data").ToString());
                    return jsonData;
                }
            }
            else
            {
                JObject data = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                if (data.GetValue("error_code").ToString() == "Unauthorized")
                {
                    await AuthentificationService.Refresh();
                    Console.WriteLine("AddTime problème de Token");
                }
                else
                {
                    Console.WriteLine("AddTime ne fonctionne pas");
                }
            }
            return null;
        }

        public static async Task<TimeItem> UpdateTime(int projectId, int taskId, int timeId, DateTime startTime, DateTime endTime)
        {
            HttpClient client = new HttpClient();

            TimeItem time = new TimeItem()
            {
                StartTime = startTime,
                EndTime = endTime,
            };

            var body = JsonConvert.SerializeObject(time);

            var resquest = new HttpRequestMessage()
            {
                RequestUri = new Uri(Dtos.Urls.HOST + "/api/v1/projects/" + projectId.ToString() + "/tasks" + taskId.ToString() + "/times"+timeId.ToString()),
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
                    TimeItem jsonData = JsonConvert.DeserializeObject<TimeItem>(data.GetValue("data").ToString());
                    return jsonData;
                }
            }
            else
            {
                JObject data = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                if (data.GetValue("error_code").ToString() == "Unauthorized")
                {
                    await AuthentificationService.Refresh();
                    Console.WriteLine("UpdateTime problème de Token");
                }
                else
                {
                    Console.WriteLine("UpdateTime ne fonctionne pas");
                }
            }
            return null;
        }

        public static async Task<bool> DeleteTime(int projectId, int taskId, int timeId)
        {
            HttpClient client = new HttpClient();

            HttpRequestMessage resquest = new HttpRequestMessage()
            {
                RequestUri = new Uri(Dtos.Urls.HOST + "/api/v1/projects/" + projectId.ToString() + "/tasks" + taskId.ToString() + "/times" + timeId.ToString()),
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
