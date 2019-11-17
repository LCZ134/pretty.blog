using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pretty.Core.Domain.Blogs;

namespace Pretty.Data.Mapping.Blogs
{
    public class BlogPostMap : PrettyEntityTypeConfiguration<BlogPost>
    {
        public override void Configure(EntityTypeBuilder<BlogPost> builder)
        {
            builder.ToTable(nameof(BlogPost));
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Title).IsRequired();
            builder.Property(i => i.Content).IsRequired();
            builder.Property(i => i.MetaKeywords).HasMaxLength(400);

            builder
                .HasOne(i => i.User)
                .WithMany()
                .HasForeignKey(i => i.UserId);

            base.Configure(builder);
        }
    }
}
