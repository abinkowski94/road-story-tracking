using System;
using RoadStoryTracking.WebApi.Business.Models.Comment;
using RoadStoryTracking.WebApi.Business.Models.Responses;

namespace RoadStoryTracking.WebApi.Business.Logic.Services.CommentService
{
    public interface ICommentService
    {
        BaseResponse AddComment(Comment comment, string userId);

        BaseResponse GetCommentsForMarker(Guid markerId);

        BaseResponse RemoveComment(Guid commentId, string userId);

        BaseResponse UpdateComment(Comment comment, string userId);
    }
}