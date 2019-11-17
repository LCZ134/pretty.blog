using Microsoft.AspNetCore.Mvc;
using Pretty.Core.Domain.Blogs;
using Pretty.Services.Blogs.Dto;
using Pretty.Services.Dto;
using Pretty.WebFramework.Controller;
using Pretty.WebFramework.Factories;
using Pretty.WebFramework.Filters;
using Pretty.WebFramework.Models;
using System.Collections.Generic;

namespace Pretty.Web.Controllers
{
    public class BlogController : PrettyController
    {
        private IBlogModelFactory _blogModelFactory;

        public BlogController(IBlogModelFactory blogModelFactory)
        {
            _blogModelFactory = blogModelFactory;
        }

        [Route("api/[controller]")]
        public Paged<BlogPostDto> Get(BlogPagingFilterModel command)
        {
            return _blogModelFactory.PrepareBlogPostModel(command);
        }

        [HttpPost]
        [Route("api/[controller]")]
        [TypeFilter(typeof(AuthFilter), Arguments = new object[] { Policy.Admin })]
        public ActResult<string> Post([FromForm] InsertBlogPostModel model)
        {
            return _blogModelFactory.PrepareInsertBlogPostModel(model);
        }

        [HttpPatch]
        [Route("api/[controller]")]
        [TypeFilter(typeof(AuthFilter), Arguments = new object[] { Policy.Admin })]
        public ActResult<string> Patch(PatchBlogPostModel model)
        {
            return _blogModelFactory.PreparePatchBlogPostModel(model);
        }

        [HttpPatch]
        [Route("api/[controller]/[action]")]
        [TypeFilter(typeof(AuthFilter), Arguments = new object[] { Policy.Admin })]
        public ActResult<string> TriggerHidden([FromForm]PatchBlogPostModel model)
        {
            return _blogModelFactory.UpdatePostHidden(model);
        }

        [HttpPatch]
        [Route("api/[controller]/[action]")]
        [TypeFilter(typeof(AuthFilter), Arguments = new object[] { Policy.Admin })]
        public ActResult<string> TriggerTop([FromForm]PatchBlogPostModel model)
        {
            return _blogModelFactory.UpdatePostTop(model);
        }

        [HttpGet]
        [Route("api/[controller]/hots")]
        public IEnumerable<BlogPostDto> Hots(int count = 6)
        {
            return _blogModelFactory.PrepareBlogPostModelByHot(count);
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public BlogPostModel Get(string id)
        {
            return _blogModelFactory.PrepareBlogPostModelById(id);
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        [TypeFilter(typeof(AuthFilter), Arguments = new object[] { Policy.Admin })]
        public ActResult<string> DeleteBlogPost(string id)
        {
            return _blogModelFactory.DeleteBlogPost(id);
        }

    }
}
