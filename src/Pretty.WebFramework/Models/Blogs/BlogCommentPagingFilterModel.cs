using System;
using System.Collections.Generic;
using System.Text;

namespace Pretty.WebFramework.Models
{
    public class BlogCommentPagingFilterModel : IPagingable
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public string UserId { get; set; }

        public string BlogPostId { get; set; }

        public string ParentId { get; set; }

        public string UserName { get; set; }

        public bool ShowHidden { get; set; }

    }
}
