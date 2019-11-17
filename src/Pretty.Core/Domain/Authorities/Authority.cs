using Pretty.Core.Domain.Users;
using System;

namespace Pretty.Core.Domain.Authorities
{
    public class Authority : BaseEntity
    {
        public string UserId { get; set; }

        public string Token { get; set; }

        public DateTime? Expire { get; set; }

        public User User { get; set; }
    }
}
