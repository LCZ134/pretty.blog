using Pretty.Services.Blogs.Dto;
using Pretty.Services.Dto;
using Pretty.WebFramework.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pretty.WebFramework.Factories
{
    public interface IBlogTagFactory
    {
        ActResult<string> InsertBlogTag(string entity);

        ActResult<string> DeleteBlogTag(string blogTagId);

        IEnumerable<BlogTagMeta> PrepareBlogPostTagMetas();

        Paged<BlogTagMeta> PrepareBlogTagMetas(BlogTagModel model);

        ActResult<string> UpdateBlogTag(BlogTagUpModel model);

    }
}
