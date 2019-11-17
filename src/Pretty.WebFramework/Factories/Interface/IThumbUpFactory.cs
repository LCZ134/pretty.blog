using Pretty.Services.Blogs.Dto;
using Pretty.Services.Dto;
using Pretty.WebFramework.Models;

namespace Pretty.WebFramework.Factories
{
    public interface IThumbUpFactory
    {
        ActResult<Paged<BlogPostDto>> PrepareBlogPostModel(ThumbUpPagingFilter filter);

        bool IsThumbUp(string id);

        ActResult<string> ToggleThumbUp(string id);
    }
}
