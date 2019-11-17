using Pretty.Core.Domain.Users;
using System;

namespace Pretty.Core.Domain.Messages
{
    public class Message : BaseEntity
    {
        public string Type { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
