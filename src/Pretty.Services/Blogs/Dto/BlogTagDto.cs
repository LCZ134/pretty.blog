using Pretty.Services.Dto;
using System;

namespace Pretty.Services.Blogs.Dto
{
    public class BlogTagDto : BaseDto
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public DateTime? CreateOn { get; set; }
    }
}
