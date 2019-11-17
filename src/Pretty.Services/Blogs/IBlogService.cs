using Pretty.Core.Domain.Blogs;
using Pretty.Services.Blogs.Dto;
using Pretty.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Pretty.Services.Blogs
{
    public interface IBlogService
    {

        #region BlogComment

        Paged<BlogCommentDto> GetComments(string userId = null, string blogPostId = null,
string parentId = null, DateTime? from = null, DateTime? to = null, string commentText = null,
            int pageIndex = 0, int pageSize = int.MaxValue,string UserName=null, bool showHidden = false);

        BlogCommentDto InsertBlogComment(BlogComment entity);

        int UpdateCommentVisibility(string blogCommentId, bool show = true);

        int DeleteBlogComment(string blogCommentId);


        #endregion

        #region BlogPost

        BlogPost GetBlogPostById(string id);

        Paged<BlogPostDto> GetBlogPosts(
            string keyword,
            string tags,
            DateTime? dateFrom,
            DateTime? dateTo,
            int pageIndex = 0,
            int pageSize = int.MaxValue
            );

        IEnumerable<BlogPostDto> GetBlogPostsByHot(int count);

        Paged<BlogPostDto> GetBlogPostsBywhere(
            string userId = null,
            string Title = null,
            string tags = null,
            DateTime? from = null,
            DateTime? to = null,
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            bool isHidden = false,
            bool isTop = false);

        int InsertBlogPost(BlogPost entity);

        int UpdateBlogPost(BlogPost entity, IEnumerable<string> feileds);

        int DeleteBlogPost(string blogPostId);

        string GetBlogPostId(Expression<Func<BlogPost, bool>> where);

        BlogPost GetBlogPostId(string blogPostId);

        int UpdatePostVisibility(string blogPostId, bool show = true);

        int UpdatePostTop(string blogPostId, bool show = true);

        #endregion

        #region BlogTag

        IEnumerable<BlogTagMeta> GetBlogPostTagMetas();

        int InsertBlogTag(BlogTag entity);

        int DeleteBlogTag(string blogTagId);

        Paged<BlogTagMeta> GetBlogTagMetas(
            string Title,
            int pageIndex = 0,
            int pageSize = int.MaxValue
            );

        int UpdateBlogTag(BlogTag entity, IEnumerable<string> feileds);

        #endregion

        #region BlogPostTag

        int InsertBlogPostTag(BlogPostTag blogPostTag);

        int DeletePostTagByBlogId(string blogPostId);


        #endregion

    }
}
