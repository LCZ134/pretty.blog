using System;

namespace Pretty.WebFramework.Models
{
    public class BlogPagingFilterModel : IPagingable
    {
        public string Keyword { get; set; }

        public string Tags { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }
    }
}
