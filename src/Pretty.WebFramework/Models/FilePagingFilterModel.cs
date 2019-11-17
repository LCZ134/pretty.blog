using System;

namespace Pretty.WebFramework.Models
{
    public class FilePagingFilterModel: IPagingable
    {
        public string UserId { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }
    }
}
