using Pretty.Core.Domain.Users;
using Pretty.Services.Dto;
using Pretty.Services.Users.Dto;
using System;

namespace Pretty.Services.Blogs.Dto
{
    public class BlogCommentDto: EntityDto
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
        /// Comment Create Date
        /// </summary>
        public DateTime? CreateOn { get; set; }

        /// <summary>
        /// Comment publisher identity
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Comment publisher
        /// </summary>
        public virtual UserDto User { get; set; }

        public short IsHidden { get; set; }

        public int ChildCount { get; set; }

    }
}
