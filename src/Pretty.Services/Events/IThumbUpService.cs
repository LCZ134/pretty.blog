using Pretty.Services.Blogs.Dto;
using Pretty.Services.Dto;

namespace Pretty.Services.Events
{
    public interface IThumbUpService
    {
        Paged<BlogPostDto> CurrentUserPost(string userId, int pageIndex, int pageSize);

        bool ThumbUp(string blogPostId, string userId);

        bool ThumbDown(string blogPostId, string userId);

        bool IsThumbUp(string blogPostId, string userId);
    }
}
