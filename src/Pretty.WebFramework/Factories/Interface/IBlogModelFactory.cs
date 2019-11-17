using Pretty.Core.Domain.Blogs;
using Pretty.Services.Blogs.Dto;
using Pretty.Services.Dto;
using Pretty.WebFramework.Models;
using System.Collections.Generic;

namespace Pretty.WebFramework.Factories
{
    public interface IBlogModelFactory
    {
        Paged<BlogPostDto> PrepareBlogPostModel(BlogPagingFilterModel command);

        Paged<BlogCommentDto> PrepareBlogCommentModel(BlogCommentPagingFilterModel command);

        BlogComment PrepareCommentPostModel(CommentPostModel comment);

        BlogPostModel PrepareBlogPostModelById(string id);

        IEnumerable<BlogPostDto> PrepareBlogPostModelByHot(int count);

        ActResult<string> PrepareInsertBlogPostModel(InsertBlogPostModel model);

        ActResult<string> PreparePatchBlogPostModel(PatchBlogPostModel model);

        ActResult<string> DeleteBlogComment(string blogCommentId);

        ActResult<string> DeleteBlogPost(string blogPostId);

        ActResult<string> UpdateBlogComment(CommentUpdateModel comment);

        ActResult<string> UpdatePostHidden(PatchBlogPostModel model);

        ActResult<string>  UpdatePostTop(PatchBlogPostModel model);

    }
}
