using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pretty.Core.Domain.Blogs;

namespace Pretty.Data.Mapping.Blogs
{
    public class BlogPostTagMap : PrettyEntityTypeConfiguration<BlogPostTag>
    {
        public override void Configure(EntityTypeBuilder<BlogPostTag> builder)
        {
            builder.ToTable(nameof(BlogPostTag));

            builder.HasKey(i => new { i.BlogPostId, i.BlogTagId });

            builder
                .HasOne(pt => pt.BlogPost)
                .WithMany(p => p.PostTags)
                .HasForeignKey(pt => pt.BlogPostId)
                .OnDelete(DeleteBehavior.Cascade);


            builder
                .HasOne(pt => pt.BlogTag)
                .WithMany(t => t.PostTags)
                .HasForeignKey(pt => pt.BlogTagId)
                .OnDelete(DeleteBehavior.Cascade);

            base.Configure(builder);
        }
    }
}
