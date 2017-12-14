using RoadStoryTracking.WebApi.Business.BusinessModels.Comment;
using RoadStoryTracking.WebApi.Business.BusinessModels.Responses;
using System;
using System.Threading.Tasks;

namespace RoadStoryTracking.WebApi.Business.CommentService
{
    public interface ICommentService
    {
        Task<BaseResponse> GetCommentsForMarker(Guid markerId);

        Task<BaseResponse> AddComment(Comment comment, string userId);

        Task<BaseResponse> RemoveComment(Guid commentId, string userId);

        Task<BaseResponse> UpdateComment(Comment comment, string userId);
    }
}
