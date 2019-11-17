using System;
using System.Collections.Generic;
using System.Text;

namespace Pretty.WebFramework.Models
{
    public class UserPagingFilterModel : IPagingable
    {
        public string Name { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }
    }
}
