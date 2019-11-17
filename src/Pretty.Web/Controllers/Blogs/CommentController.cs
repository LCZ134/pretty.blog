using Microsoft.AspNetCore.Mvc;
using Pretty.Core;
using Pretty.Core.Chains;
using Pretty.Services.Blogs;
using Pretty.Services.Blogs.Dto;
using Pretty.Services.Dto;
using Pretty.WebFramework.Controller;
using Pretty.WebFramework.Factories;
using Pretty.WebFramework.Filters;
using Pretty.WebFramework.Models;

namespace Pretty.Web.Controllers
{

    [Route("api/[controller]")]
    public class CommentController : PrettyController
    {
        private IBlogModelFactory _blogModelFactory;
        private IBlogService _blogService;

        public CommentController(
            IBlogModelFactory blogModelFactory,
            IBlogService blogService)
        {
            _blogModelFactory = blogModelFactory;
            _blogService = blogService;
        }

        [HttpGet]
        public Paged<BlogCommentDto> Get(BlogCommentPagingFilterModel command)
        {
            return _blogModelFactory.PrepareBlogCommentModel(command);
        }

        [HttpPost]
        [TypeFilter(typeof(AuthFilter), Arguments = new object[] { Policy.Logged })]
        public ActResult<BlogCommentDto> Post(CommentPostModel comment)
        {
            var entity = _blogModelFactory.PrepareCommentPostModel(comment);
            Singletion.Get<CommentHandleChain>().Execute(entity);
            var obj = _blogService.InsertBlogComment(entity);
            return new ActResult<BlogCommentDto>(
                obj == null ? Services.Dto.StatusCode.Failed : Services.Dto.StatusCode.Success,
                obj != null ? "评论成功" : "评论失败")
            { Extends = obj };
        }

        [HttpDelete]
        [TypeFilter(typeof(AuthFilter), Arguments = new object[] { Policy.Admin })]
        public ActResult<string> Delete(string blogCommentId)
        {
            return _blogModelFactory.DeleteBlogComment(blogCommentId);
        }

        [HttpPatch]
        [TypeFilter(typeof(AuthFilter), Arguments = new object[] { Policy.Admin })]
        public ActResult<string> Patch([FromForm]CommentUpdateModel comment)
        {
            return _blogModelFactory.UpdateBlogComment(comment);
        }

    }
}
