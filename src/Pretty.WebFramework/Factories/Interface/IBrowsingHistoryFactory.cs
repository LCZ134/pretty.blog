using Pretty.Core.Domain.Events;
using Pretty.Services.Blogs.Dto;
using Pretty.Services.Dto;
using Pretty.WebFramework.Models;

namespace Pretty.WebFramework.Factories
{
    public interface IBrowsingHistoryFactory
    {
        ActResult<Paged<BlogPostDto>> PrepareHistoryModel(HistoryPagingFilter filter);
        bool InsertHistory(string blogPostId);
    }
}
