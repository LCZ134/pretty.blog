using Pretty.Core.Domain.Roles;
using Pretty.Services.Dto;
using Pretty.Services.Roles.Dto;
using System.Collections.Generic;

namespace Pretty.Services.Roles
{
    public interface IRoleService
    {
        Paged<RolesDto> GetAll(int pageIndex, int pageSize);

        int AddRole(Role role);

        int UpdateRole(Role role, IEnumerable<string> feileds);

        int UpdateRole(Role role);

        int DeleteRole(string id);

        Role GetRoleByUser(string userId);
    }
}
