using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Pretty.Core;
using Pretty.Core.Domain.Roles;
using Pretty.Services.Dto;
using Pretty.Services.Roles;
using Pretty.WebFramework.Jwt;

namespace Pretty.WebFramework.Filters
{
    public enum Policy
    {
        NoAuth = 0,
        Logged = 1,
        Admin = 2,
    }

    public class AuthFilter : IActionFilter
    {
        private Policy _policy;
        private IWorkContext _workContext;
        private IRoleService _roleService;

        public AuthFilter(Policy policy, IWorkContext workContext, IRoleService roleService)
        {
            _policy = policy;
            _workContext = workContext;
            _roleService = roleService;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            switch (_policy)
            {
                case Policy.Logged:
                    if (_workContext.User == null)
                    {
                        context.Result = new JsonResult(
                            new ActResult<string>(StatusCode.NeedLogin, "请先登录，再进行操作"))
                        { StatusCode = 200 };
                    }
                    break;
                case Policy.Admin:
                    if (_workContext.User == null ||
                        _roleService.GetRoleByUser(_workContext.User.Id)?.RoleLevel != (int)PrettyRoles.Admin)
                    {
                        context.Result = new JsonResult(
                            new ActResult<string>(StatusCode.AuthFailed, "权限不足"))
                        { StatusCode = 403 };
                    }
                    break;
            }
        }

    }
}
