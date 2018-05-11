using RoadStoryTracking.WebApi.Business.Models.Comment;
using RoadStoryTracking.WebApi.Business.Models.Exceptions;
using RoadStoryTracking.WebApi.Business.Models.Responses;
using RoadStoryTracking.WebApi.Data.Repositories;
using System;
using System.Collections.Generic;

namespace RoadStoryTracking.WebApi.Business.Logic.Services.CommentService
{
    public class CommentService : BaseService, ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public BaseResponse AddComment(Comment comment, string userId)
        {
            var dbComment = LocalMapper.Map<Data.Models.Comment>(comment);
            dbComment.ApplicationUserId = userId;
            dbComment = _commentRepository.AddComment(dbComment);

            return new SuccessResponse<Comment>(LocalMapper.Map<Comment>(dbComment));
        }

        public BaseResponse GetCommentsForMarker(Guid markerId)
        {
            var comments = _commentRepository.GetCommentsForMarker(markerId);

            return new SuccessResponse<List<Comment>>(LocalMapper.Map<List<Comment>>(comments));
        }

        public BaseResponse RemoveComment(Guid commentId, string userId)
        {
            var comment = _commentRepository.GetCommentForUser(commentId, userId);
            if (comment == null)
            {
                return new ErrorResponse(new CustomApplicationException($"Cannot find comment with id: {commentId}"));
            }

            var result = _commentRepository.RemoveComment(comment);
            return new SuccessResponse<Comment>(LocalMapper.Map<Comment>(result));
        }

        public BaseResponse UpdateComment(Comment comment, string userId)
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
        }
    }
}