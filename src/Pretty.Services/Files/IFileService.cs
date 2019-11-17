using Pretty.Core.Domain.Blogs;
using Pretty.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pretty.Services.Files
{
    public interface IFileService
    {
        int InsertFile(IEnumerable<BlogFile> files);

        Paged<BlogFile> GetBlogFiles(
            string userId = null,
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            DateTime? from = null,
            DateTime? to = null);

        int DeleteBlogFile(string blogFileId);

        BlogFile GetBlogFile(string blogFileId);
    }
}
