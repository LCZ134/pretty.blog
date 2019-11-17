using System;
using System.Collections.Generic;

namespace Pretty.Core.Domain.Users
{
    public class User : BaseEntity
    {
        /// <summary>
        /// User Name
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// User email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User like
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// User gender
        /// </summary>
        public int? Gender { get; set; }

        /// <summary>
        /// Blog Access Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Salt of security
        /// </summary>
        public string Salt { get; set; }

        /// <summary>
        /// User avatar url
        /// </summary>
        public string AvatarUrl { get; set; }
        
        public short Online { get; set; }

        public string RoleId { get; set; }

        public virtual Roles.Role Role { get; set; }

    }
}
