using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pretty.Core.Domain.Danmus;

namespace Pretty.Data.Mapping.Danmus
{
    public class DanmuMap : PrettyEntityTypeConfiguration<Danmu>
    {
        public override void Configure(EntityTypeBuilder<Danmu> builder)
        {
            builder.ToTable(nameof(Danmu));
            builder.HasKey(i => i.Id);


            base.Configure(builder);
        }
    }
}
