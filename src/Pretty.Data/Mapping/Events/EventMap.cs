using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pretty.Core.Domain.Event;

namespace Pretty.Data.Mapping.Events
{
    public class EventMap : PrettyEntityTypeConfiguration<Event>
    {
        public override void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable(nameof(Event));
            builder.HasKey(i => i.Id);

            builder
                .HasOne(i => i.User)
                .WithMany()
                .HasForeignKey(i => i.UserId)
                .IsRequired();

            base.Configure(builder);
        }
    }
}
