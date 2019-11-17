using Pretty.Core.Domain.Users;
using System;
using System.Collections.Generic;

namespace Pretty.Core.Domain.Blogs
{
    public class BlogPost : BaseEntity, IHiddeable
    {
        /// <summary>
        /// Blog title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Blog Content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Blog Describe
        /// </summary>
        public string Describe { get; set; }

        /// <summary>
        /// Blog MdContent
        /// </summary>
        public string MdContent { get; set; }

        /// <summary>
        /// Blog Tags
        /// </summary>
        public virtual ICollection<BlogPostTag> PostTags { get; set; }

        /// <summary>
        /// Create User Identity
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Blog view count
        /// </summary>
        public int View { get; set; }

        /// <summary>
        /// Blog like count
        /// </summary>
        public int Like { get; set; }

        /// <summary>
        /// Show the blog on home top
        /// </summary>
        public short IsTop { get; set; }

        /// <summary>
        /// Banner Image Url
        /// </summary>
        public string BannerUrl { get; set; }

        /// <summary>
        /// Blog Seo Meta Keywords
        /// </summary>
        public string MetaKeywords { get; set; }

        /// <summary>
        /// Blog Updaet On Date
        /// </summary>
        public DateTime? UpdateOn { get; set; }

        /// <summary>
        /// Blog Create User
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// show the blog, if value is true 
        /// </summary>
        public short IsHidden { get; set; }

        public int CommentCount { get; set; }

    }
}
