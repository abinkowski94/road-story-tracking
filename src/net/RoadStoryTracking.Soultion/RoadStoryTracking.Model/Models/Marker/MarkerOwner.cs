using Newtonsoft.Json;

namespace RoadStoryTracking.Model.Models.Marker
{
    public class MarkerOwner
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }
    }
}