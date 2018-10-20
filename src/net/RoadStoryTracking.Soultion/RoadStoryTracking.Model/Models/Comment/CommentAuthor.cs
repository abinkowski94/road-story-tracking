using Newtonsoft.Json;

namespace RoadStoryTracking.Model.Models.Comment
{
    public class CommentAuthor
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