using Newtonsoft.Json;
using System;

namespace RoadStoryTracking.Model.Models
{
    public class TokenInfo
    {
        [JsonProperty("expirationDate")]
        public DateTime ExpirationDate { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }
    }
}