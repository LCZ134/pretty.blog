using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pretty.Core.Domain.Customers;
using Pretty.Core.Domain.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pretty.Data.Mapping.Customers
{
    class CustomerMap : PrettyEntityTypeConfiguration<Customer>
    {
        public override void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");

            base.Configure(builder);
        }
    }
}
