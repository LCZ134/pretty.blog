using Pretty.Core;
using Pretty.Core.Domain.Roles;
using Pretty.Core.Extends;
using Pretty.Services.Dto;
using Pretty.Services.Roles;
using Pretty.Services.Roles.Dto;
using Pretty.WebFramework.Factories.Interface;
using Pretty.WebFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pretty.WebFramework.Factories
{
    public class RoleModelFactory : IRoleModelFactory
    {

        private IRoleService _roleService;
        private IWorkContext _webContext;
        public RoleModelFactory(IRoleService roleSerivce, IWorkContext webContext)
        {
            _roleService = roleSerivce;
            _webContext = webContext;
        }

        public ActResult<string> DeleteRole(string id)
        {
            int result = _roleService.DeleteRole(id);
            return ActResultFactory.GetActResult(result);
        }

        public Paged<RolesDto> GetAll(Pagingable command)
        {
            return _roleService.GetAll(command.PageIndex, command.PageSize);
        }

        public ActResult<string> InsertRole(RoleInsertModel model)
        {
            Role entity = new Role()
            {
                RoleName = model.RoleName,
                RoleLevel= model.RoleLevel,
                RoleRemark=model.RoleRemark,
                CreateUserid = _webContext.User.Id,
                CreateOn = DateTime.Now
            };

            int result = _roleService.AddRole(entity);
            return ActResultFactory.GetActResult(result);
        }

        public ActResult<string> UpdateRole(Role role, IEnumerable<string> feileds)
        {
            int result = _roleService.UpdateRole(role, feileds);
            return ActResultFactory.GetActResult(result);
        }

        public ActResult<string> UpdateRole(RoleUpdataModel model)
        {
            var entity = model.Copy<Role>();
            var updateProps = Utils.GetPropNames(model);

            int result = _roleService.UpdateRole(
                    entity,
                    updateProps
                );

            return ActResultFactory.GetActResult(result);
        }
    }
}
