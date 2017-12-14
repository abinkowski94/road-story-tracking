using Newtonsoft.Json;
using System;

namespace RoadStoryTracking.Model.Models.Comment
{
    public class Comment
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("markerId")]
        public Guid MarkerId { get; set; }

        [JsonProperty("commentAuthor")]
        public CommentAuthor CommentAuthor { get; set; }

        [JsonProperty("createDate")]
        public DateTimeOffset CreateDate { get; set; }

        [JsonProperty("modificationDate")]
        public DateTimeOffset ModificationDate { get; set; }
    }
}
