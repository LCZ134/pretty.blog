using System;
using System.Collections.Generic;
using System.Text;

namespace Pretty.Core.Domain.Blogs
{
    public class BlogFile : BaseEntity
    {
        public BlogFile()
        {
            CreateOn = DateTime.Now;
        }

        public string Name { get; set; }

        public string Path { get; set; }

        public string Suffix { get; set; }

        public string UserId { get; set; }

        public long FileSize { get; set; }
    }
}
