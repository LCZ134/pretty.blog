using Microsoft.AspNetCore.Mvc;
using Pretty.Services.Blogs.Dto;
using Pretty.Services.Dto;
using Pretty.WebFramework.Controller;
using Pretty.WebFramework.Factories;
using Pretty.WebFramework.Filters;
using Pretty.WebFramework.Models;

namespace Pretty.Web.Controllers
{
    public class ThumbUpController : PrettyController
    {
        private IThumbUpFactory _thumbUpFactory;

        public ThumbUpController(IThumbUpFactory thumbUpFactory)
        {
            _thumbUpFactory = thumbUpFactory;
        }

        [TypeFilter(typeof(AuthFilter), Arguments = new object[] { Policy.Logged })]
        [Route("api/[controller]")]
        public ActResult<Paged<BlogPostDto>> Get(ThumbUpPagingFilter filter)
        {
            return _thumbUpFactory.PrepareBlogPostModel(filter);
        }


        [HttpPost]
        [TypeFilter(typeof(AuthFilter), Arguments = new object[] { Policy.Logged })]
        [Route("api/[controller]/{id}")]
        public ActResult<string> Post(string id)
        {
            return _thumbUpFactory.ToggleThumbUp(id);
        }
    }
}
