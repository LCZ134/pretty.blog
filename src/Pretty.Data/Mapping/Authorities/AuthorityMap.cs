using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pretty.Core.Domain.Authorities;

namespace Pretty.Data.Mapping.Authorities
{
    public  class AuthorityMap : PrettyEntityTypeConfiguration<Authority>
    {
        public override void Configure(EntityTypeBuilder<Authority> builder)
        {
            builder.ToTable(nameof(Authority));
            builder.HasKey(blogPost => blogPost.Id);

            builder
                .HasOne(i => i.User)
                .WithMany()
                .HasForeignKey(i => i.UserId)
                .IsRequired();

            base.Configure(builder);
        }
    }
}
