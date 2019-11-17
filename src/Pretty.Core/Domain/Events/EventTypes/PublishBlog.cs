using Pretty.Core.Domain.Blogs;
using Pretty.Core.Domain.Users;

namespace Pretty.Core.Domain.Events.EventTypes
{
    public class PublishBlog : Event
    {
        public PublishBlog(BlogPost entity)
        {
            Entity = entity;
        }

        public BlogPost Entity { get; set; }

    }
}
