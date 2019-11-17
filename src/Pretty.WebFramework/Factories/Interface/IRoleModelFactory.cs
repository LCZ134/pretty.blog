using Pretty.Core.Domain.Roles;
using Pretty.Services.Dto;
using Pretty.Services.Roles.Dto;
using Pretty.WebFramework.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pretty.WebFramework.Factories
{
    public interface IRoleModelFactory
    {

        Paged<RolesDto> GetAll(Pagingable command);

        ActResult<string> InsertRole(RoleInsertModel model);

        ActResult<string> UpdateRole(RoleUpdataModel model);

        ActResult<string> DeleteRole(string id);

    }
}
