using System;
using System.Collections.Generic;

namespace Pretty.Core.Domain.Blogs
{
    public class BlogTag : BaseEntity
    {
        /// <summary>
        /// Title Name
        /// </summary>
        public string Title { get; set; }

        public virtual ICollection<BlogPostTag> PostTags { get; set; }
    }
}
