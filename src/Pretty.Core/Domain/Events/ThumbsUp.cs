using Pretty.Core.Domain.Blogs;
using System;

namespace Pretty.Core.Domain.Events
{
    public class ThumbsUp : BaseEntity
    {
        public string BlogPostId { get; set; }

        public string UserId { get; set; }

        public BlogPost BlogPost { get; set; }
    }
}
