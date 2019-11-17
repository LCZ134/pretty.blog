using System;
using System.Collections.Generic;
using System.Text;

namespace Pretty.WebFramework.Models.Events
{
    public class EventModel
    {
        public EventModel()
        {
            PageSize = 10;
            IsALL = false;
        }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public bool IsALL { get; set; }

    }
}
