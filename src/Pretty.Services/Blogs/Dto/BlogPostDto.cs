using Pretty.Core.Domain.Blogs;
using Pretty.Services.Dto;
using Pretty.Services.Users.Dto;
using System;
using System.Collections.Generic;

namespace Pretty.Services.Blogs.Dto
{
    public class BlogPostDto : EntityDto
    {
        /// <summary>
        /// Blog title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Blog Content
        /// </summary>
        //public string Content { get; set; }

        /// <summary>
        /// Blog Describe
        /// </summary>
        public string Describe { get; set; }

        /// <summary>
        /// Blog MdContent
        /// </summary>
        //public string MdContent { get; set; }

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

        public bool IsLiked { get; set; }

        /// <summary>
        /// Blog comment count
        /// </summary>
        public int CommentCount { get; set; }

        /// <summary>
        /// Banner Image Url
        /// </summary>
        public string BannerUrl { get; set; }

        /// <summary>
        /// Blog Seo Meta Keywords
        /// </summary>
        public string MetaKeywords { get; set; }

        /// <summary>
        /// Blog Create On Date
        /// </summary>
        public DateTime CreateOn { get; set; }

        /// <summary>
        /// Blog Updaet On Date
        /// </summary>
        public DateTime? UpdateOn { get; set; }

        /// <summary>
        /// Blog Create User
        /// </summary>
        public virtual UserDto User { get; set; }

        public ICollection<BlogTagDto> Tags { get; set; }

        /// <summary>
        /// Show the blog on home top
        /// </summary>
        public short IsTop { get; set; }

        /// <summary>
        /// show the blog, if value is true 
        /// </summary>
        public short IsHidden { get; set; }

    }
}
