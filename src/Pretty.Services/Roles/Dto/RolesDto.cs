using Pretty.Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pretty.Services.Roles.Dto
{
    public class RolesDto
    {
        public string Id { get; set; }

        public string RoleName { get; set; }

        public int RoleLevel { get; set; }

        public string RoleRemark { get; set; }

        public int UserCount { get; set; }

        public User User { get; set; }

        public DateTime? CreateOn { get; set; }

    }
}
