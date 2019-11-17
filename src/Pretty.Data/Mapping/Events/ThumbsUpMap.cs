using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pretty.Core.Domain.Events;

namespace Pretty.Data.Mapping.Events
{
    public class ThumbsUpMap : PrettyEntityTypeConfiguration<ThumbsUp>
    {
        public override void Configure(EntityTypeBuilder<ThumbsUp> builder)
        {
            builder.ToTable(nameof(ThumbsUp));
            builder.HasKey(i => i.Id);


            base.Configure(builder);
        }
    }
}
