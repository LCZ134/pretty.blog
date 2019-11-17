using System;

namespace Pretty.Services.Danmus.Dto
{
    public class DanmuDto
    {
        public string Id { get; set; }

        public string Color { get; set; }

        public float FontSize { get; set; }

        public string Content { get; set; }

        public string Type { get; set; }

        public DateTime CreateOn { get; set; }
    }
}
