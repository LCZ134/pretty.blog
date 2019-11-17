using Pretty.Core;
using Pretty.Core.Domain.Blogs;
using Pretty.Core.Extends;
using Pretty.Services.Blogs;
using Pretty.Services.Blogs.Dto;
using Pretty.Services.Dto;
using Pretty.Services.Events;
using Pretty.WebFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pretty.WebFramework.Factories
{
    public class BlogModelFactory : IBlogModelFactory
    {
        private IBlogService _blogService;
        private IWorkContext _webContext;
        private IThumbUpService _thumbUpService;
        public BlogModelFactory(
            IBlogService blogService,
            IWorkContext webContext,
            IThumbUpService thumbUpService)
        {
            _blogService = blogService;
            _webContext = webContext;
            _thumbUpService = thumbUpService;
        }

        public Paged<BlogPostDto> PrepareBlogPostModel(BlogPagingFilterModel command)
        {
            if (command.PageSize == 0)
                command.PageSize = 10;

            var pageList = _blogService.GetBlogPosts(
                command.Keyword,
                command.Tags,
                command.DateFrom,
                command.DateTo,
                command.PageIndex,
                command.PageSize
                );


            return pageList;
        }

        public Paged<BlogCommentDto> PrepareBlogCommentModel(BlogCommentPagingFilterModel command)
        {
            if (command.PageSize == 0)
                command.PageSize = 10;

            var pageList = _blogService.GetComments(
                command.UserId,
                command.BlogPostId,
                command.ParentId,
                command.From,
                command.To,
                null,
                command.PageIndex,
                command.PageSize,
                command.UserName,
                command.ShowHidden
                );

            return pageList;
        }




        public BlogComment PrepareCommentPostModel(CommentPostModel comment)
        {
            return new BlogComment
            {
                Content = comment.Content,
                UserId = _webContext.User?.Id,
                IsHidden = 1,
                CreateOn = DateTime.Now,
                BlogPostId = comment.BlogPostId,
                ParentId = comment.ParentId
            };
        }

        public BlogPostModel PrepareBlogPostModelById(string id)
        {
            var blogPost = _blogService.GetBlogPostById(id);
            var result = blogPost.Copy<BlogPostModel>();

            result.Tags = blogPost.PostTags.Select(i => i.BlogTag.Copy<BlogTagDto>()).ToList();
            result.IsLiked = _thumbUpService.IsThumbUp(id, _webContext.User?.Id);
            return result;
        }

        public IEnumerable<BlogPostDto> PrepareBlogPostModelByHot(int count)
        {
            return _blogService.GetBlogPostsByHot(count);
        }


        public ActResult<string> PrepareInsertBlogPostModel(InsertBlogPostModel model)
        {
            var entity = model.Copy<BlogPost>();
            entity.CreateOn = DateTime.Now;
            entity.UserId = _webContext.User?.Id;
            int result = _blogService.InsertBlogPost(entity);

            #region 添加tag
            //先获取id
            string blogPostId = _blogService.GetBlogPostId(m => m.Title == model.Title);
            string[] tags = model.Tags.Split(',');

            foreach (var item in tags)
            {
                var blogPostTag = new BlogPostTag() { BlogPostId = blogPostId, BlogTagId = item };
                _blogService.InsertBlogPostTag(blogPostTag);
            }
            #endregion

            return ActResultFactory.GetActResult(result);
        }

        public ActResult<string> PreparePatchBlogPostModel(PatchBlogPostModel model)
        {
            #region 添加标签
            if (!string.IsNullOrEmpty(model.Tags))
            {
                var a = _blogService.DeletePostTagByBlogId(model.Id);

                string[] tags = model.Tags.Split(',');
                foreach (var item in tags)
                {
                    var blogPostTag = new BlogPostTag() { BlogPostId = model.Id, BlogTagId = item };
                    var b = _blogService.InsertBlogPostTag(blogPostTag);
                }
            }
            #endregion

            var entity = model.Copy<BlogPost>();
            entity.User = _webContext.User;
            entity.UpdateOn = DateTime.Now;
            var updateProps = Utils.GetPropNames(model).Where(i => !i.Equals("Id"));

            // 导航属性不能设置 IsModified            
            int result = _blogService.UpdateBlogPost(
                    entity,
                    updateProps.Append("PostTags").Append("User")
                );
            return ActResultFactory.GetActResult(result);
        }


        //删除评论
        public ActResult<string> DeleteBlogComment(string blogCommentId)
        {
            int result = _blogService.DeleteBlogComment(blogCommentId);
            return ActResultFactory.GetActResult(result);
        }

        public ActResult<string> DeleteBlogPost(string blogPostId)
        {

            int result = _blogService.DeleteBlogPost(blogPostId);
            return ActResultFactory.GetActResult(result);
        }

        public ActResult<string> UpdateBlogComment(CommentUpdateModel comment)
        {
            var result = _blogService.UpdateCommentVisibility(comment.Id, comment.IsHidden > 0);
            return ActResultFactory.GetActResult(result);
        }

        public ActResult<string> UpdatePostHidden(PatchBlogPostModel model)
        {
            var result = _blogService.UpdatePostVisibility(model.Id, model.IsHidden > 0);
            return ActResultFactory.GetActResult(result);
        }

        public ActResult<string> UpdatePostTop(PatchBlogPostModel model)
        {
            var result = _blogService.UpdatePostTop(model.Id, model.IsTop > 0);
            return ActResultFactory.GetActResult(result);
        }
    }
}
