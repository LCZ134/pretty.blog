using Pretty.Core;
using Pretty.Core.Domain.Blogs;
using Pretty.Core.Extends;
using Pretty.Services.Blogs;
using Pretty.Services.Blogs.Dto;
using Pretty.Services.Dto;
using Pretty.WebFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pretty.WebFramework.Factories
{
    public class BlogTagFactory : IBlogTagFactory
    {

        private IBlogService _blogService;

        public BlogTagFactory(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public IEnumerable<BlogTagMeta> PrepareBlogPostTagMetas()
        {
            return _blogService.GetBlogPostTagMetas();
        }

        public ActResult<string> InsertBlogTag(string title)
        {
            BlogTag entity = new BlogTag()
            {
                Title = title,
                CreateOn = DateTime.Now
            };

            int result = _blogService.InsertBlogTag(entity);
            return ActResultFactory.GetActResult(result);
        }

        public ActResult<string> DeleteBlogTag(string blogTagId)
        {
            int result = _blogService.DeleteBlogTag(blogTagId);
            return ActResultFactory.GetActResult(result);
        }

        public Paged<BlogTagMeta> PrepareBlogTagMetas(BlogTagModel model)
        {
            return _blogService.GetBlogTagMetas(model.Title, model.PageIndex, model.PageSize);
        }

        public ActResult<string> UpdateBlogTag(BlogTagUpModel model)
        {
            var entity = model.Copy<BlogTag>();

            var updateProps = Utils.GetPropNames(model).Where(i => !i.Equals("Id")); ;

            int result = _blogService.UpdateBlogTag(
                entity,
                updateProps.Append("PostTags")
            );

            return ActResultFactory.GetActResult(result);
        }
    }
}
