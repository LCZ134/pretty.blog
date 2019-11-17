using System.ComponentModel.DataAnnotations.Schema;

namespace Pretty.Core.Domain.Blogs
{
    public class BlogPostTag : BaseEntity
    {
        public string BlogPostId { get; set; }

        public string BlogTagId { get; set; }

        public BlogPost BlogPost { get; set; }

        public BlogTag BlogTag { get; set; }
    }
}
