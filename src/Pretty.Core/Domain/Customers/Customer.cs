using Pretty.Core.Domain.Users;
using System;

namespace Pretty.Core.Domain.Customers
{
    public class Customer : BaseEntity
    {
        public string UserId { get; set; }

        public string EventName { get; set; }

        public virtual User User { get; set; }
    }
}
