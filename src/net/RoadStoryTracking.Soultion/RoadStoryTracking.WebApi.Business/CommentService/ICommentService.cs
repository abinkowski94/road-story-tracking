using RoadStoryTracking.WebApi.Business.BusinessModels.Comment;
using RoadStoryTracking.WebApi.Business.BusinessModels.Responses;
using System;

namespace RoadStoryTracking.WebApi.Business.CommentService
{
    public interface ICommentService
    {
        BaseResponse AddComment(Comment comment, string userId);

        BaseResponse GetCommentsForMarker(Guid markerId);

        BaseResponse RemoveComment(Guid commentId, string userId);

        BaseResponse UpdateComment(Comment comment, string userId);
    }
}