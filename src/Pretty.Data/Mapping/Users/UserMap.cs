using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pretty.Core.Domain.Blogs;
using Pretty.Core.Domain.Users;

namespace Pretty.Data.Mapping.Blog
{
    public class UserMap : PrettyEntityTypeConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));
            builder.HasKey(i => i.Id);

            builder
                .HasOne(i => i.Role)
                .WithMany()
                .HasForeignKey(i => i.RoleId);

            base.Configure(builder);
        }
    }
}
