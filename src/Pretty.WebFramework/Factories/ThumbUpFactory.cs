using Pretty.Core;
using Pretty.Services.Blogs.Dto;
using Pretty.Services.Dto;
using Pretty.Services.Events;
using Pretty.WebFramework.Models;

namespace Pretty.WebFramework.Factories
{
    public class ThumbUpFactory : IThumbUpFactory
    {
        private IThumbUpService _thumbUpService;
        private IWorkContext _workContext;

        public ThumbUpFactory(
            IThumbUpService thumbUpService,
            IWorkContext workContext)
        {
            _thumbUpService = thumbUpService;
            _workContext = workContext;
        }

        public bool ThumbUp(string blogPostId)
        {
            var userId = _workContext.User.Id;
            return _thumbUpService.ThumbUp(blogPostId, userId);
        }

        public bool ThumbDown(string id)
        {
            var userId = _workContext.User.Id;
            return _thumbUpService.ThumbDown(id, userId);
        }

        public bool IsThumbUp(string id)
        {
            var userId = _workContext.User.Id;
            return _thumbUpService.IsThumbUp(id, userId);
        }

        public ActResult<Paged<BlogPostDto>> PrepareBlogPostModel(ThumbUpPagingFilter filter)
        {
            return new ActResult<Paged<BlogPostDto>>(
                StatusCode.Success,
                "获取成功",
                _thumbUpService.CurrentUserPost(
                    _workContext.User?.Id,
                    filter.PageIndex,
                    filter.PageSize));
        }

        public ActResult<string> ToggleThumbUp(string id)
        {
            if (IsThumbUp(id))
            {
                ThumbDown(id);
                return new ActResult<string>(StatusCode.Success, "取消点赞成功");
            }
            else
            {
                ThumbUp(id);
                return new ActResult<string>(StatusCode.Success, "点赞成功");
            }
        }
    }
}
