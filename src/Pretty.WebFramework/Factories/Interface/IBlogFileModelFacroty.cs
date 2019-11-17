using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pretty.Core.Domain.Blogs;
using Pretty.Services.Dto;
using Pretty.WebFramework.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Pretty.WebFramework.Factories
{
    public interface IBlogFileModelFacroty
    {
        Paged<BlogFile> GetBlogFiles(FilePagingFilter filter);

        ActResult<string> DeleteBlogFile(string fileId);

        IEnumerable<ActResult<BlogFile>> SaveFiles(IFormFileCollection files);

        IActionResult GetFile(FetchFileArgs args);
    }
}
