using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pretty.Core.Domain.Blogs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pretty.Data.Mapping.Blogs
{
    class BlogTagMap : PrettyEntityTypeConfiguration<BlogTag>
    {
        public override void Configure(EntityTypeBuilder<BlogTag> builder)
        {
            builder.ToTable(nameof(BlogTag));
            builder.HasKey(i => i.Id);

            base.Configure(builder);
        }
    }
}
