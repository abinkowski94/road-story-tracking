using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadStoryTracking.Model.Models.Comment;
using RoadStoryTracking.WebApi.Business.CommentService;
using RoadStoryTracking.WebApi.Extensions;
using System;
using System.Collections.Generic;
using BMC = RoadStoryTracking.WebApi.Business.BusinessModels.Comment;

namespace RoadStoryTracking.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CommentController : BaseController
    {
        private readonly ICommentService _commentService;

        public CommentController(IServiceProvider serviceProvider, ICommentService commentService) : base(serviceProvider)
        {
            _commentService = commentService;
        }

        [Authorize]
        [HttpPost("[action]")]
        public IActionResult AddComment([FromBody] Comment comment)
        {
            var mappedComment = LocalMapper.Map<BMC.Comment>(comment);
            var response = _commentService.AddComment(mappedComment, Requestor.User.Id);

            return response.GetActionResult<BMC.Comment, Comment>(this);
        }

        [HttpGet("[action]")]
        public IActionResult GetCommentsForMarker(Guid markerId)
        {
            var response = _commentService.GetCommentsForMarker(markerId);
            return response.GetActionResult<List<BMC.Comment>, List<Comment>>(this);
        }

        [Authorize]
        [HttpDelete("[action]")]
        public IActionResult RemoveComment(Guid commentId)
        {
            var response = _commentService.RemoveComment(commentId, Requestor.User.Id);
            return response.GetActionResult<BMC.Comment, Comment>(this);
        }

        [Authorize]
        [HttpPut("[action]")]
        public IActionResult UpdateComment([FromBody] Comment comment)
        {
            var mappedComment = LocalMapper.Map<BMC.Comment>(comment);
            var response = _commentService.UpdateComment(mappedComment, Requestor.User.Id);

            return response.GetActionResult<BMC.Comment, Comment>(this);
        }
    }
}