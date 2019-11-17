using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pretty.Core.Domain.Blogs;

namespace Pretty.Data.Mapping.Blogs
{
    public class BlogCommentMap : PrettyEntityTypeConfiguration<BlogComment>
    {
        public override void Configure(EntityTypeBuilder<BlogComment> builder)
        {
            builder.ToTable(nameof(BlogComment));
            builder.HasKey(blogPost => blogPost.Id);

            builder.Property(i => i.UserId).IsRequired();
            builder.Property(i => i.Content).IsRequired();

            builder
                .HasOne(i => i.User)
                .WithMany()
                .HasForeignKey(i => i.UserId);


            base.Configure(builder);
        }
    }
}
