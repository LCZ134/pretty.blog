using Pretty.Core.Domain.Events;
using Pretty.Services.Blogs.Dto;
using Pretty.Services.Dto;
using System;

namespace Pretty.Services.Events
{
    public interface IBrowsingHistoryService
    {
        int InsertHistory(BrowsingHistory history);

        Paged<BlogPostDto> GetHistory(
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            string userId = null,
            string blogPostId = null,
            DateTime? from = null,
            DateTime? to = null);
        bool HasHistory(string blogPostId, string id);
    }
}
