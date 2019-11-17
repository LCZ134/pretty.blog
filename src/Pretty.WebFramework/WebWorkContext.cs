using Microsoft.AspNetCore.Http;
using Pretty.Core;
using Pretty.Core.Domain.Users;
using Pretty.Services.Authorities;
using Pretty.Services.Users;
using Pretty.WebFramework.Jwt;

namespace Pretty.WebFramework
{
    public class WebWorkContext : IWorkContext
    {
        private IHttpContextAccessor _httpContextAccessor;
        private IUserService _userService;
        private IAuthorityService _authorityService;

        public WebWorkContext(
            IHttpContextAccessor httpContextAccessor,
            IUserService userService,
            IAuthorityService authorityService)
        {
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _authorityService = authorityService;
        }

        private User _user;

        public User User
        {
            get
            {
                if (_user == null)
                {
                    var token = _httpContextAccessor.HttpContext.Request.Cookies["token"];
                    //var userId = _authorityService.GetUserIdByToken(token);
                    var userId = "";

                    var user = JwtTools.Decodes(token);
                    if (user!=null)
                       userId = user["UserId"].ToString();
                   
                    _user = _userService.GetUserById(userId);
                }

                return _user;
            }
        }

    }
}
