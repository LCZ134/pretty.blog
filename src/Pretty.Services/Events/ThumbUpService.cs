using Microsoft.EntityFrameworkCore;
using Pretty.Core.Data;
using Pretty.Core.Domain.Blogs;
using Pretty.Core.Domain.Events;
using Pretty.Services.Blogs.Dto;
using Pretty.Services.Dto;
using Pretty.Services.Extends;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pretty.Services.Events
{
    public class ThumbUpService : IThumbUpService
    {
        private IRepository<BlogPost> _blogPostRepos;
        private IRepository<ThumbsUp> _thumbsUpRepos;

        public ThumbUpService(
            IRepository<BlogPost> blogPostRepos,
            IRepository<ThumbsUp> thumbsUpRepos)
        {
            _blogPostRepos = blogPostRepos;
            _thumbsUpRepos = thumbsUpRepos;
        }

        public int UpdateBlogPost(BlogPost entity, IEnumerable<string> props)
        {
            return _blogPostRepos.Update(entity, props);
        }

        public bool ThumbUp(string blogPostId, string userId)
        {
            if (IsThumbUp(blogPostId, userId))
                return false;

            _thumbsUpRepos.Insert(new ThumbsUp
            {
                BlogPostId = blogPostId,
                UserId = userId,
                CreateOn = DateTime.Now
            });

            var blogPost = _blogPostRepos.GetById(blogPostId);
            blogPost.Like++;
            return _blogPostRepos.Update(blogPost) > 0;
        }

        public bool ThumbDown(string blogPostId, string userId)
        {
            var thumbsUp = _thumbsUpRepos.Table
                .FirstOrDefault(i => i.BlogPostId == blogPostId && i.UserId == userId);
            if (thumbsUp == null)
                return false;

            _thumbsUpRepos.Delete(thumbsUp);

            var blogPost = _blogPostRepos.GetById(blogPostId);
            blogPost.Like--;
            return _blogPostRepos.Update(blogPost) > 0;
        }

        public bool IsThumbUp(string blogPostId, string userId)
        {
            return _thumbsUpRepos.Table
                       .Count(i => i.BlogPostId == blogPostId && i.UserId == userId) > 0;
        }

        public Paged<BlogPostDto> CurrentUserPost(
            string userId,
            int pageIndex = 0,
            int pageSize = 10)
        {
            var query = _thumbsUpRepos.Table
                .Where(i => i.UserId == userId)
                .Include(i => i.BlogPost)
                .Select(i => i.BlogPost);
            return new PagedList<BlogPost>(query, pageIndex, pageSize)
                .GetDto<BlogPost, BlogPostDto>();
        }
    }
}
