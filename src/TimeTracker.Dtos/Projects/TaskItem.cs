using System.Collections.Generic;
using Newtonsoft.Json;

namespace TimeTracker.Dtos.Projects
{
    public class TaskItem
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("times")]
        public List<TimeItem> Times { get; set; }

        [JsonIgnore]
        public long sumTimes { get; set; }
        

        public void setSum()
        {
            foreach (var item in Times)
            {
                this.sumTimes += (long)(item.EndTime - item.StartTime).TotalSeconds;
            }
        }
    }
}