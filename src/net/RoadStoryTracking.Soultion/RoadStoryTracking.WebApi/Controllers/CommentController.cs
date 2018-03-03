using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadStoryTracking.Model.Models.Comment;
using RoadStoryTracking.WebApi.Business.CommentService;
using RoadStoryTracking.WebApi.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<IActionResult> AddComment([FromBody] Comment comment)
        {
            var mappedComment = LocalMapper.Map<BMC.Comment>(comment);
            var response = await _commentService.AddComment(mappedComment, Requestor.User.Id);

            return response.GetActionResult<BMC.Comment, Comment>(this);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetCommentsForMarker(Guid markerId)
        {
            var response = await _commentService.GetCommentsForMarker(markerId);
            return response.GetActionResult<List<BMC.Comment>, List<Comment>>(this);
        }

        [Authorize]
        [HttpDelete("[action]")]
        public async Task<IActionResult> RemoveComment(Guid commentId)
        {
            var response = await _commentService.RemoveComment(commentId, Requestor.User.Id);
            return response.GetActionResult<BMC.Comment, Comment>(this);
        }

        [Authorize]
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateComment([FromBody] Comment comment)
        {
            var mappedComment = LocalMapper.Map<BMC.Comment>(comment);
            var response = await _commentService.UpdateComment(mappedComment, Requestor.User.Id);

            return response.GetActionResult<BMC.Comment, Comment>(this);
        }
    }
}