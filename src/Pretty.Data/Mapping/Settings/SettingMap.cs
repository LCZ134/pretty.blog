using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pretty.Core.Domain.Settings;

namespace Pretty.Data.Mapping.Settings
{
    public class SettingMap : PrettyEntityTypeConfiguration<Setting>
    {
        public override void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.ToTable(nameof(Setting));
            builder.HasKey(i => i.Id);


            base.Configure(builder);
        }
    }
}
