using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Pretty.Core;
using Pretty.Core.Data;
using Pretty.Core.Domain.Blogs;
using Pretty.Core.Domain.Events;
using Pretty.Core.Domain.Events.EventTypes;
using Pretty.Core.Enums;
using Pretty.Core.Extends;
using Pretty.Services.Blogs.Dto;
using Pretty.Services.Dto;
using Pretty.Services.Events;
using Pretty.Services.Extends;

namespace Pretty.Services.Blogs
{
    public class BlogService : IBlogService
    {
        private IRepository<BlogPost> _blogPostRepos;
        private IRepository<BlogComment> _blogCommentRepos;
        private IRepository<BlogTag> _blogTagRepos;
        private IRepository<ThumbsUp> _thumbsUpRepos;
        private IRepository<BlogPostTag> _blogPostTagRepos;
        private IEventPublisher _eventPublisher;

        public BlogService(IRepository<BlogPost> blogPostRepos,
            IRepository<BlogComment> blogCommentRepos,
            IRepository<BlogTag> blogTagRepos,
            IRepository<ThumbsUp> thumbsUpRepos,
            IRepository<BlogPostTag> blogPostTagRepos,
            IRepository<BlogFile> blogFileRepos,
            IEventPublisher eventPublisher)
        {
            _blogPostRepos = blogPostRepos;
            _blogCommentRepos = blogCommentRepos;
            _blogTagRepos = blogTagRepos;
            _thumbsUpRepos = thumbsUpRepos;
            _blogPostTagRepos = blogPostTagRepos;
            _eventPublisher = eventPublisher;
        }


        #region BlogComment
        public Paged<BlogCommentDto> GetComments(string userId = null, string blogPostId = null,
            string parentId = null, DateTime? from = null, DateTime? to = null, string commentText = null,
            int pageIndex = 0, int pageSize = int.MaxValue, string UserName = null, bool showHidden = false)
        {
            var query = _blogCommentRepos.Table;

            if (!string.IsNullOrEmpty(userId))
                query = query.Where(i => i.UserId == userId);
            if (!string.IsNullOrEmpty(blogPostId))
                query = query.Where(i => i.BlogPostId == blogPostId);
            if (!string.IsNullOrEmpty(parentId))
            {
                query = query.Where(i => i.ParentId == parentId);
            }
            else
            {
                query = query.Where(i => string.IsNullOrEmpty(i.ParentId));
            }
            if (from.HasValue)
                query = query.Where(i => from.Value <= i.CreateOn);
            if (to.HasValue)
                query = query.Where(i => to.Value >= i.CreateOn);
            if (!string.IsNullOrEmpty(commentText))
                query = query.Where(i => EF.Functions.Like(i.Content, $"%{commentText}%"));
            if (!string.IsNullOrEmpty(UserName))
                query = query.Where(i => i.User.NickName.Contains(UserName));
            if (!showHidden)
                query = query.Where(i => i.IsHidden == 1);
            query = query.OrderByDescending(i => i.CreateOn);

            var blogPosts = new PagedList<BlogComment>(
                query.Include(i => i.User),
                pageIndex,
                pageSize
                );

            return blogPosts.GetDto<BlogComment, BlogCommentDto>((source, target) =>
            {
                target.ChildCount = _blogCommentRepos.Table.Count(i => i.ParentId == source.Id);
                return target;
            });
        }

        public BlogCommentDto InsertBlogComment(BlogComment entity)
        {
            //_blogPostRepos.Update(new BlogPost { Id = entity.BlogPostId,c}, )
            _blogCommentRepos.Insert(entity);
            var blogPost = _blogPostRepos.GetById(entity.BlogPostId);
            blogPost.CommentCount++;
            _blogPostRepos.Update(blogPost);

            return _blogCommentRepos.Table
                .Include(i => i.User)
                .FirstOrDefault(i => i.Id == entity.Id)
                ?.Copy<BlogCommentDto>();
        }

        public int UpdateCommentVisibility(string blogCommentId, bool show = true)
        {
            short ishidden = (short)(show ? 1 : 0);

            var blogComment = _blogCommentRepos.Table
                .FirstOrDefault(i => i.Id == blogCommentId);
            if (blogComment == null)
                return 0;
            if (blogComment.IsHidden == ishidden)
                return 0;
            blogComment.IsHidden = ishidden;
            return _blogCommentRepos.Update(blogComment);
        }

        public int DeleteBlogComment(string blogCommentId)
        {
            return _blogCommentRepos.Delete(_blogCommentRepos.GetById(blogCommentId));
        }
        #endregion

        #region BlogPost
        public BlogPost GetBlogPostById(string id)
        {
            var blogPost = _blogPostRepos.Table
                .Include(i => i.User)
                .Include(i => i.PostTags)
                .ThenInclude(i => i.BlogTag)
                .Where(i => i.Id == id)
                .FirstOrDefault();

            return blogPost;
        }

        public Paged<BlogPostDto> GetBlogPosts(
            string keyword,
            string tags,
            DateTime? dateFrom,
            DateTime? dateTo,
            int pageIndex,
            int pageSize)
        {
            var query = _blogPostRepos.Table;

            if (tags?.Length > 0)
            {
                var tagArr = tags.Split(',');
                query = _blogPostTagRepos.Table
                    .Where(i => tagArr.Contains(i.BlogTagId))
                    .Include(i => i.BlogPost)
                    .Select(i => i.BlogPost);
            }

            if (dateFrom.HasValue)
                query = query.Where(i => dateFrom.Value <= i.CreateOn);
            if (dateTo.HasValue)
                query = query.Where(i => dateTo.Value >= i.CreateOn);
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(i => i.Title.Contains(keyword) || i.Content.Contains(keyword));

            query = query.OrderByDescending(i => i.CreateOn);
            var incQuery = query
                .Include(i => i.User)
                .Include(i => i.PostTags)
                .ThenInclude(i => i.BlogTag);

            var blogPosts = new PagedList<BlogPost>(
                incQuery,
                pageIndex,
                pageSize
            );

            var result = blogPosts.GetDto<BlogPost, BlogPostDto>((source, target) =>
            {
                target.Tags = source
                    .PostTags
                    .Select(i => i
                        .BlogTag
                        .Copy<BlogTagDto>())
                    .ToList();
                return target;
            });

            return result;
        }
      

        public IEnumerable<BlogPostDto> GetBlogPostsByHot(int count)
        {
            var query = _blogPostRepos.Table;
            query.OrderByDescending(i => i.Like).Take(count);

            return query.Select(Utils.Copy<BlogPostDto>);
        }

        public int InsertBlogPost(BlogPost entity)
        {
            if (entity.IsHidden == (short)BoolEnum.False)
            {
                _eventPublisher.Publish(new PublishBlog(entity));
            }
            return _blogPostRepos.Insert(entity);
        }

        public int UpdateBlogPost(BlogPost entity, IEnumerable<string> props)
        {
            return _blogPostRepos.Update(entity, props);
        }

        Paged<BlogPostDto> IBlogService.GetBlogPostsBywhere(string userId, string Title, string tags, DateTime? from, DateTime? to, int pageIndex, int pageSize, bool isHidden, bool isTop)
        {
            var query = _blogPostRepos.Table;

            if (!string.IsNullOrEmpty(userId))
                query = query.Where(i => i.UserId == userId);
            if (!string.IsNullOrEmpty(Title))
                query = query.Where(i => EF.Functions.Like(i.Title, $"%{Title}%"));
            if (isHidden)
                query = query.Where(i => i.IsHidden == 0);
            if (isTop)
                query = query.Where(i => i.IsTop != 0);
            if (from.HasValue)
                query = query.Where(i => from.Value <= i.CreateOn);
            if (to.HasValue)
                query = query.Where(i => to.Value >= i.CreateOn);
            var incQuery = query
            .Include(i => i.User)
            .Include(i => i.PostTags)
            .ThenInclude(i => i.BlogTag);

            var blogPosts = new PagedList<BlogPost>(
                incQuery,
                pageIndex,
                pageSize
            );

            var result = blogPosts.GetDto<BlogPost, BlogPostDto>((source, target) =>
            {
                target.Tags = source
                    .PostTags
                    .Select(i => i
                        .BlogTag
                        .Copy<BlogTagDto>())
                    .ToList();
                return target;
            });
            return result;
        }

        public int DeleteBlogPost(string blogPostId)
        {
            return _blogPostRepos.Delete(_blogPostRepos.GetById(blogPostId));
        }

        public string GetBlogPostId(Expression<Func<BlogPost, bool>> where)
        {
            return _blogPostRepos.Table.Where(where).First().Id;
        }

        public BlogPost GetBlogPostId(string blogPostId)
        {
            return _blogPostRepos.GetById(blogPostId);
        }

        public int UpdatePostVisibility(string blogPostId, bool show = true)
        {
            short ishidden = (short)(show ? 1 : 0);
            var blogPost = _blogPostRepos.Table
                .FirstOrDefault(i => i.Id == blogPostId);
            if (blogPost == null)
                return 0;
            if (blogPost.IsHidden == ishidden)
                return 0;
            blogPost.IsHidden = ishidden;
            return _blogPostRepos.Update(blogPost);
        }

        public int UpdatePostTop(string blogPostId, bool show = true)
        {
            //将其他的置顶的文章取消置顶
            // CancelPostTap();
            short isTop = (short)(show ? 1 : 0);
            var blogPost = _blogPostRepos.Table
                .FirstOrDefault(i => i.Id == blogPostId);
            if (blogPost == null)
                return 0;
            if (blogPost.IsTop == isTop)
                return 0;
            blogPost.IsHidden = isTop;
            return _blogPostRepos.Update(blogPost); 
        }

        public int CancelPostTop()
        {
            var blogPost = _blogPostRepos.Table
                .FirstOrDefault(i => i.IsTop > 0);
            if (blogPost == null)
                return 0;
            blogPost.IsTop = 0;
            return _blogPostRepos.Update(blogPost);
        }

        #endregion

        #region BlogTag
        public IEnumerable<BlogTagMeta> GetBlogPostTagMetas()
        {
            var query = _blogTagRepos
                .Table
                .Include(i => i.PostTags);

            return query
                .ToList()
                .Select(i => new BlogTagMeta
                {
                    Id = i.Id,
                    Title = i.Title,
                    CitationCount = i.PostTags?.Count()
                });
        }

        public int InsertBlogTag(BlogTag entity)
        {
            return _blogTagRepos.Insert(entity);
        }

        public int DeleteBlogTag(string blogTagId)
        {
            return _blogTagRepos.Delete(_blogTagRepos.GetById(blogTagId));
        }

        public Paged<BlogTagMeta> GetBlogTagMetas(string Title, int pageIndex = 0, int pageSize = 10)
        {

            var query = _blogTagRepos.Table;
            if (!string.IsNullOrEmpty(Title))
                query = query.Where(i => i.Title.Contains(Title));

            var result = query.Include(i => i.PostTags).ToList()
                .Select(i => new BlogTagMeta
                {
                    Id = i.Id,
                    Title = i.Title,
                    CitationCount = i.PostTags?.Count()
                }).ToList();
            pageSize = pageSize > 0 ? pageSize : 10;
            pageIndex = pageIndex <= 0 ? 0 : pageIndex;

            return new PagedList<BlogTagMeta>(result, pageIndex, pageSize).GetDto();

        }

        public int UpdateBlogTag(BlogTag entity, IEnumerable<string> feileds)
        {
            return _blogTagRepos.Update(entity, feileds);
        }

        #endregion

        #region BlogPostTag
        public int InsertBlogPostTag(BlogPostTag blogPostTag)
        {
            return _blogPostTagRepos.Insert(blogPostTag);
        }

        public int DeletePostTagByBlogId(string blogPostId)
        {
            return _blogPostTagRepos.Delete(_blogPostTagRepos.Table
                .Where(s => s.BlogPostId == blogPostId));
        }

        #endregion

    }
}
