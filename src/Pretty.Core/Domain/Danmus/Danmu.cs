using System;
using System.Collections.Generic;
using System.Text;

namespace Pretty.Core.Domain.Danmus
{
    public class Danmu : BaseEntity
    {
        public Danmu()
        {
            CreateOn = DateTime.Now;
        }

        public string Color { get; set; }

        public float FontSize { get; set; }

        public string Content { get; set; }

        public string Type { get; set; }

        public string UserId { get; set; }


    }
}
