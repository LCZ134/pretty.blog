using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pretty.Data.Mapping.Role
{
    class RoleMap : PrettyEntityTypeConfiguration<Core.Domain.Roles.Role>
    {
        public override void Configure(EntityTypeBuilder<Core.Domain.Roles.Role> builder)
        {
            builder.ToTable(nameof(Core.Domain.Roles.Role));
            builder.HasKey(i => i.Id);

            base.Configure(builder);
        }
    }
}
