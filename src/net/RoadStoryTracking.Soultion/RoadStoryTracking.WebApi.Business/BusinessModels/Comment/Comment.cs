using System;

namespace RoadStoryTracking.WebApi.Business.BusinessModels.Comment
{
    public class Comment
    {
        public CommentAuthor CommentAuthor { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public Guid Id { get; set; }
        public Guid MarkerId { get; set; }
        public DateTimeOffset ModificationDate { get; set; }
        public string Text { get; set; }
    }
}