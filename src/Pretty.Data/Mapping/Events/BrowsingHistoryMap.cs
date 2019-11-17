using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pretty.Core.Domain.Events;

namespace Pretty.Data.Mapping.Events
{
    class BrowsingHistoryMap : PrettyEntityTypeConfiguration<BrowsingHistory>
    {
        public override void Configure(EntityTypeBuilder<BrowsingHistory> builder)
        {
            builder.ToTable(nameof(BrowsingHistory));
            builder.HasKey(i => i.Id);

            base.Configure(builder);
        }
    }
}
