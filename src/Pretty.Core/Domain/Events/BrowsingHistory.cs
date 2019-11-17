using Pretty.Core.Domain.Blogs;
using System;

namespace Pretty.Core.Domain.Events
{
    public class BrowsingHistory : BaseEntity
    {
        public string UserId { get; set; }

        public string BlogPostId { get; set; }

        public BlogPost BlogPost { get; set; }
    }
}
