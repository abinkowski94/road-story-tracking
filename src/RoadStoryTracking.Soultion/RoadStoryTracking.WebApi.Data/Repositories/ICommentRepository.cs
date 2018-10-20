using RoadStoryTracking.WebApi.Data.Models;
using System;
using System.Collections.Generic;

namespace RoadStoryTracking.WebApi.Data.Repositories
{
    public interface ICommentRepository : IDisposable
    {
        Comment AddComment(Comment comment);

        Comment GetCommentForUser(Guid markerId, string userId);

        List<Comment> GetCommentsForMarker(Guid markerId);

        Comment RemoveComment(Comment comment);

        Comment UpdateComment(Comment comment);
    }
}