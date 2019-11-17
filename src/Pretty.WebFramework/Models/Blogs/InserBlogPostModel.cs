using System.Collections.Generic;

namespace Pretty.WebFramework.Models
{
    public class InsertBlogPostModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string MdContent { get; set; }

        public string MetaKeywords { get; set; }

        public string Describe { get; set; }

        public string BannerUrl { get; set; }

        public string Tags { get; set; }

        public short IsTop { get; set; }

        public short IsHidden { get; set; }

    }
}
