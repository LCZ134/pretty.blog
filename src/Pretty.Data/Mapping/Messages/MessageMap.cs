using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pretty.Core.Domain.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pretty.Data.Mapping.Messages
{
    class MessageMap : PrettyEntityTypeConfiguration<Message>
    {
        public override void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable(nameof(Message));
            builder.HasKey(i => i.Id);

            base.Configure(builder);
        }
    }
}
