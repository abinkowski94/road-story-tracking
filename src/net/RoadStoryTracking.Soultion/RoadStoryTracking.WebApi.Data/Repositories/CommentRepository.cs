﻿using Microsoft.EntityFrameworkCore;
using RoadStoryTracking.WebApi.Data.Context;
using RoadStoryTracking.WebApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RoadStoryTracking.WebApi.Data.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly RoadStoryTrackingDbContext _dbContext;

        public CommentRepository(RoadStoryTrackingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Comment> GetCommentsForMarker(Guid markerId)
        {
            return _dbContext.Comments
                .Include(c => c.ApplicationUser)
                .Where(c => c.MarkerId == markerId)
                .OrderBy(c => c.CreateDate)
                .ToList();
        }

        public Comment AddComment(Comment comment)
        {
            _dbContext.Comments.Add(comment);
            _dbContext.SaveChanges();

            return comment;
        }

        public Comment RemoveComment(Comment comment)
        {
            _dbContext.Comments.Remove(comment);
            _dbContext.SaveChanges();

            return comment;
        }

        public Comment UpdateComment(Comment comment)
        {
            _dbContext.SaveChanges();

            return comment;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
