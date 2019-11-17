using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pretty.Core.Domain.Navigations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pretty.Data.Mapping.Navigations
{
    public class NavigationMap : PrettyEntityTypeConfiguration<Navigation>
    {
        public override void Configure(EntityTypeBuilder<Navigation> builder)
        {
            builder.ToTable(nameof(Navigation));
            builder.HasKey(i => i.Id);

            base.Configure(builder);
        }
    }
}
