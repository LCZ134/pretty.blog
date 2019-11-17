using Pretty.Core;
using Pretty.Core.Domain.Events;
using Pretty.Services.Blogs.Dto;
using Pretty.Services.Dto;
using Pretty.Services.Events;
using Pretty.WebFramework.Models;

namespace Pretty.WebFramework.Factories
{
    public class BrowsingHistoryFactory : IBrowsingHistoryFactory
    {
        private IBrowsingHistoryService _browsingHistoryService;
        private IWorkContext _workContext;

        public BrowsingHistoryFactory(
            IBrowsingHistoryService browsingHistoryService,
            IWorkContext workContext)
        {
            _browsingHistoryService = browsingHistoryService;
            _workContext = workContext;
        }

        public bool InsertHistory(string blogPostId)
        {
            var user = _workContext.User;

            if (user == null)
                return false;
            if (_browsingHistoryService.HasHistory(blogPostId, user.Id))
                return false;

            return _browsingHistoryService.InsertHistory(
                new BrowsingHistory
                {
                    BlogPostId = blogPostId,
                    UserId = user.Id
                }) > 0;
        }

        public ActResult<Paged<BlogPostDto>> PrepareHistoryModel(HistoryPagingFilter filter)
        {
            var user = _workContext.User;

            if (user == null)
            {
                return new ActResult<Paged<BlogPostDto>>(StatusCode.NeedLogin, "请先登录");
            }

            return new ActResult<Paged<BlogPostDto>>(
                StatusCode.Success,
                "获取成功",
                _browsingHistoryService.GetHistory(
                    filter.PageIndex,
                    filter.PageSize,
                    user.Id));
        }
    }
}
