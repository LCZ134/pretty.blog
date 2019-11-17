using Pretty.Core.Domain.Blogs;

namespace Pretty.Core.Chains
{
    /// <summary>
    /// Handle Comment Chain
    /// </summary>
    public interface ICommentHandler
    {
        bool Execute(BlogComment comment);
    }
}
