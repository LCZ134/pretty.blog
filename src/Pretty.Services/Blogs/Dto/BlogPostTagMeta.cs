using Pretty.Services.Dto;

namespace Pretty.Services.Blogs.Dto
{
    public class BlogTagMeta: BaseDto
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public int? CitationCount{ get; set; }
    }
}
