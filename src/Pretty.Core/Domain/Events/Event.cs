using Pretty.Core.Domain.Users;
using System;

namespace Pretty.Core.Domain.Event
{
    public class Event : BaseEntity
    {
        /// <summary>
        /// Request Ip addres
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// User identity
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Request Url
        /// </summary>
        public string RequestUrl { get; set; }

        /// <summary>
        /// Referrer Url
        /// </summary>
        public string ReferrerUrl { get; set; }

        public User User { get; set; }
    }
}
