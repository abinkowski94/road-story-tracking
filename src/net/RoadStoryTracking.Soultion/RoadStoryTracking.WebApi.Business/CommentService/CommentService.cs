using RoadStoryTracking.WebApi.Business.BusinessModels.Comment;
using RoadStoryTracking.WebApi.Business.BusinessModels.Exceptions;
using RoadStoryTracking.WebApi.Business.BusinessModels.Responses;
using RoadStoryTracking.WebApi.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoadStoryTracking.WebApi.Business.CommentService
{
    public class CommentService : BaseService, ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public Task<BaseResponse> AddComment(Comment comment, string userId)
        {
            return Task.Run<BaseResponse>(() =>
            {
                var dbComment = LocalMapper.Map<Data.Models.Comment>(comment);
                dbComment.ApplicationUserId = userId;
                dbComment = _commentRepository.AddComment(dbComment);

                return new SuccessResponse<Comment>(LocalMapper.Map<Comment>(dbComment));
            });
        }

        public Task<BaseResponse> GetCommentsForMarker(Guid markerId)
        {
            return Task.Run<BaseResponse>(() =>
            {
                var comments = _commentRepository.GetCommentsForMarker(markerId);

                return new SuccessResponse<List<Comment>>(LocalMapper.Map<List<Comment>>(comments));
            });
        }

        public Task<BaseResponse> RemoveComment(Guid commentId, string userId)
        {
            return Task.Run<BaseResponse>(() =>
            {
                var comment = _commentRepository.GetCommentForUser(commentId, userId);
                if (comment == null)
                {
                    return new ErrorResponse(new CustomApplicationException($"Cannot find comment with id: {commentId}"));
                }

                var result = _commentRepository.RemoveComment(comment);
                return new SuccessResponse<Comment>(LocalMapper.Map<Comment>(result));
            });
        }

        public Task<BaseResponse> UpdateComment(Comment comment, string userId)
        {
            return Task.Run<BaseResponse>(() =>
            {
                var dbComment = _commentRepository.GetCommentForUser(comment.Id, userId);
                if (dbComment == null)
                {
                    return new ErrorResponse(new CustomApplicationException($"Cannot find comment with id: {comment.Id}"));
                }

                dbComment.Text = comment.Text;
                dbComment.ModificationDate = DateTimeOffset.UtcNow;

                var result = _commentRepository.UpdateComment(dbComment);
                return new SuccessResponse<Comment>(LocalMapper.Map<Comment>(result));
            });
        }
    }
}