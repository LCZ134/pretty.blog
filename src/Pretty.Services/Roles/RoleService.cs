using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pretty.Core.Data;
using Pretty.Core.Domain.Roles;
using Pretty.Core.Domain.Users;
using Pretty.Services.Dto;
using Pretty.Services.Extends;
using Pretty.Services.Roles.Dto;

namespace Pretty.Services.Roles
{
    public class RoleService : IRoleService
    {
        private IRepository<Role> _roleRepos;
        private IRepository<User> _userRepos;
        public RoleService(IRepository<Role> roleRepos, IRepository<User> userRepos)
        {
            _roleRepos = roleRepos;
            _userRepos = userRepos;
        }

        public int AddRole(Role role)
        {
            return _roleRepos.Insert(role);
        }

        public int DeleteRole(string id)
        {
            var query= _roleRepos.GetById(id);
            if (query == null)
                return 0;
            return _roleRepos.Delete(query);
        }

        public Paged<RolesDto> GetAll(int pageIndex, int pageSize)
        {
            var query = _roleRepos.Table;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var role = new PagedList<Role>(query, pageIndex, pageSize);
            var result = role.GetDto<Role,RolesDto>((source, target) =>
            {
                target.UserCount = GetRoleCount( source.Id);
                target.User = _userRepos.GetById(source.CreateUserid);
                return target;
            });

            return result;
        }

        public int GetRoleCount(string roleid)
        {
           return _userRepos.Table.Where(i => i.RoleId == roleid).ToList().Count;
        }

        public Role GetRoleByUser(string userId)
        {
            return _roleRepos.GetById(_userRepos.GetById(userId).RoleId);
        }

        public int UpdateRole(Role role, IEnumerable<string> feileds)
        {
            return _roleRepos.Update(role, feileds);
        }

        public int UpdateRole(Role role)
        {
            return _roleRepos.Update(role);
        }
    }
}
