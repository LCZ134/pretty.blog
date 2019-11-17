using Pretty.Core.Domain.Users;
using System;

namespace Pretty.Core.Domain.Blogs
{
    public class BlogComment : BaseEntity, IHiddeable
    {
        /// <summary>
        /// Parent identity
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// Blog post identity
        /// </summary>
        public string BlogPostId { get; set; }

        /// <summary>
        /// Comment Content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Comment publisher identity
        /// </summary>
        public string UserId { get; set; }

        public short IsHidden { get; set; }

        /// <summary>
        /// Comment publisher
        /// </summary>
        public virtual User User { get; set; }
    }
}
