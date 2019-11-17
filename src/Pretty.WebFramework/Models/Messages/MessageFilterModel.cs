using System;

namespace Pretty.WebFramework.Models
{
    public class MessageFilterModel : Pagingable
    {
        public MessageFilterModel()
        {
            this.PageSize = 10000;
        }

        public string Type { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }
    }
}
