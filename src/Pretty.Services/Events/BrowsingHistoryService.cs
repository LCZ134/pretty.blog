using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Pretty.Core.Data;
using Pretty.Core.Domain.Blogs;
using Pretty.Core.Domain.Events;
using Pretty.Services.Blogs.Dto;
using Pretty.Services.Dto;
using Pretty.Services.Extends;

namespace Pretty.Services.Events
{
    public class BrowsingHistoryService : IBrowsingHistoryService
    {
        private IRepository<BrowsingHistory> _browsingHistoryRepos;
        private IRepository<BlogPost> _blogPostRepos;

        public BrowsingHistoryService(
            IRepository<BrowsingHistory> browsingHistoryRepos,
            IRepository<BlogPost> blogPostRepos)
        {
            _browsingHistoryRepos = browsingHistoryRepos;
            _blogPostRepos = blogPostRepos;
        }


        public Paged<BlogPostDto> GetHistory(
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            string userId = null,
            string blogPostId = null,
            DateTime? from = null,
            DateTime? to = null)
        {
            var query = _browsingHistoryRepos.Table;

            if (!string.IsNullOrEmpty(userId))
                query = query.Where(i => i.UserId == userId);
            if (!string.IsNullOrEmpty(blogPostId))
                query = query.Where(i => i.BlogPostId == blogPostId);
            if (from.HasValue)
                query = query.Where(i => from.Value <= i.CreateOn);
            if (to.HasValue)
                query = query.Where(i => to.Value >= i.CreateOn);

            query = query.OrderByDescending(i => i.CreateOn);

            var history = new PagedList<BlogPost>(
                query.Include(i => i.BlogPost)
                    .Select(i => i.BlogPost)
                    .Where(i => i != null),
                pageIndex,
                pageSize
                );

            return history.GetDto<BlogPost, BlogPostDto>();
        }

        public bool HasHistory(string blogPostId, string userId)
        {
            return _browsingHistoryRepos.Table
                .Count(i => i.BlogPostId == blogPostId && i.UserId == userId) > 0;
        }

        public int InsertHistory(BrowsingHistory history)
        {
            var blogPost = _blogPostRepos.GetById(history.BlogPostId);
            blogPost.View++;
            _blogPostRepos.Update(blogPost);
            return _browsingHistoryRepos.Insert(history);
        }
    }
}
