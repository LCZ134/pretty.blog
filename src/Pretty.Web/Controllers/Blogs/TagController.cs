using Microsoft.AspNetCore.Mvc;
using Pretty.Services.Blogs.Dto;
using Pretty.Services.Dto;
using Pretty.WebFramework.Controller;
using Pretty.WebFramework.Factories;
using Pretty.WebFramework.Filters;
using Pretty.WebFramework.Models;
using System.Collections.Generic;

namespace Pretty.Web.Controllers
{
    public class TagController : PrettyController
    {
        private IBlogTagFactory _blogTagFactory;

        public TagController(IBlogTagFactory blogTagFactory)
        {
            _blogTagFactory = blogTagFactory;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public IEnumerable<BlogTagMeta> Get()
        {
            return _blogTagFactory.PrepareBlogPostTagMetas();
        }

        [HttpGet]
        [Route("api/[controller]/[action]")]
        public Paged<BlogTagMeta> GetALL(BlogTagModel model)
        {
            return _blogTagFactory.PrepareBlogTagMetas(model);
        }

        [HttpPost]
        [Route("api/[controller]")]
        [TypeFilter(typeof(AuthFilter), Arguments = new object[] { Policy.Logged })]
        public ActResult<string> Post(string Title)
        {
            return _blogTagFactory.InsertBlogTag(Title);
        }

        [HttpDelete]
        [Route("api/[controller]")]
        [TypeFilter(typeof(AuthFilter), Arguments = new object[] { Policy.Admin })]
        public ActResult<string> DeleteBlogTag(string blogTagId)
        {
            return _blogTagFactory.DeleteBlogTag(blogTagId);
        }

        //修改
        [HttpPatch]
        [Route("api/[controller]")]
        [TypeFilter(typeof(AuthFilter), Arguments = new object[] { Policy.Logged })]
        public ActResult<string> Patch(BlogTagUpModel model)
        {
            return _blogTagFactory.UpdateBlogTag(model);
        }

    }
}