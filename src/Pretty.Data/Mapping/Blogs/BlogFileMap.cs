using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pretty.Core.Domain.Blogs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pretty.Data.Mapping.Blogs
{
    class BlogFileMap : PrettyEntityTypeConfiguration<BlogFile>
    {
        public override void Configure(EntityTypeBuilder<BlogFile> builder)
        {
            builder.ToTable(nameof(BlogFile));
            builder.HasKey(i => i.Id);

            base.Configure(builder);
        }
    }
}
