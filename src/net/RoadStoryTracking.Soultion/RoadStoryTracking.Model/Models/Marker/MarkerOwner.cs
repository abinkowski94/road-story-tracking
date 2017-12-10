using Newtonsoft.Json;

namespace RoadStoryTracking.Model.Models.Marker
{
    public class MarkerOwner
    {
        [JsonProperty("firsName")]
        public string FirsName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }
    }
}