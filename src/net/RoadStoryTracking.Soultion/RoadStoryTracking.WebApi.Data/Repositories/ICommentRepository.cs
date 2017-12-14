using RoadStoryTracking.WebApi.Data.Models;
using System;
using System.Collections.Generic;

namespace RoadStoryTracking.WebApi.Data.Repositories
{
    public interface ICommentRepository : IDisposable
    {
        List<Comment> GetCommentsForMarker(Guid markerId);

        Comment AddComment(Comment comment);

        Comment RemoveComment(Comment comment);

        Comment UpdateComment(Comment comment);

        Comment GetCommentForUser(Guid markerId, string userId);
    }
}
