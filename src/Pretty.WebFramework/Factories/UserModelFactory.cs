using Pretty.Core;
using Pretty.Core.Domain.Users;
using Pretty.Core.Enums;
using Pretty.Core.Extends;
using Pretty.Services.Dto;
using Pretty.Services.Users;
using Pretty.Services.Users.Dto;
using Pretty.WebFramework.Models;
using System.Collections.Generic;
using System.Linq;

namespace Pretty.WebFramework.Factories
{
    public class UserModelFactory : IUserModelFactory
    {
        private IWorkContext _workContext;
        private IUserService _userService;

        public UserModelFactory(IWorkContext workContext, IUserService userService)
        {
            _workContext = workContext;
            _userService = userService;
        }

        public ActResult<string> AddBlackList(string id)
        {
            int result = _userService.UpdateBlackList(id, false);

            return ActResultFactory.GetActResult(result);
        }

        public Paged<UserDto> GetUserList(UserPagingFilterModel commed)
        {
            return _userService.GetAllUser(
                commed.Name,
                commed.PageIndex,
                commed.PageSize,
                commed.DateFrom,
                commed.DateTo);
        }

        public LoginResult Login(LoginFormModel loginForm)
        {
            var statusCode = LoginStatus.UserNotExists;
            string token = string.Empty;
            var user = _userService.Login(loginForm.Email, loginForm.Password,out statusCode);
            if (user!=null && statusCode== LoginStatus.Success)
                token = Jwt.JwtTools.Encode(new Dictionary<string, object>(){ { "UserId", user.Id }});
            return new LoginResult
            {
                StatusCode = statusCode,
                User = user?.Copy<UserDto>(),
                Token = token
            };

            //return _userService.Login(loginForm.Email, loginForm.Password);
        }

        public ActResult<Paged<UserDto>> PreparOnlineMember(Pagingable page)
        {
            return new ActResult<Paged<UserDto>>(StatusCode.Success,
                "获取成功",
                _userService.GetAllUser(
                    null,
                    page.PageIndex,
                    page.PageSize,
                    null, null, i => i.Online == (short)BoolEnum.True));
        }

        public ActResult<UserDto> PreparPatchUserModel(UserPatch patch)
        {
            var entity = patch.Copy<User>();
            var user = _workContext.User;

            if (string.IsNullOrEmpty(entity.Id))
            {
                 entity.Id = user?.Id;
                if (patch.NickName != user.NickName && _userService.GetUserByNickname(patch.NickName) != null)
                {
                    return new ActResult<UserDto>(StatusCode.Success, "该昵称已存在，更新失败", null);
                }
            }

            if (_userService.UpdateUser(entity, Utils.GetPropNames(patch).ToArray()))
            {
                return new ActResult<UserDto>(StatusCode.Success, "更新成功", entity.Copy<UserDto>());
            }
            return new ActResult<UserDto>(StatusCode.Success, "更新失败", null);
        }

        public ActResult<UserDto> PreparUserModel()
        {
            var curUser = _workContext.User;

            if (curUser == null)
            {
                return new ActResult<UserDto>(
                    StatusCode.NeedLogin,
                    "请先登录");
            }
            else
            {
                return new ActResult<UserDto>(
                    StatusCode.Success,
                    "获取成功",
                    curUser.Copy<UserDto>());
            }
        }

        public ActResult<LoginResult> Register(RegisterFormModel form)
        {
            if (string.IsNullOrEmpty(form.Nickname) || string.IsNullOrEmpty(form.Password))
            {
                return new ActResult<LoginResult>(StatusCode.Failed, "昵称或密码不能为空");
            }
            if (_userService.GetUserByEmail(form.Email) != null)
            {
                return new ActResult<LoginResult>(StatusCode.Failed, "该邮箱已注册");
            }
            _userService.Register(new UserDto
            {
                Email = form.Email,
                NickName = form.Nickname,
            }, form.Password);
            var loginResult = _userService.Login(form.Nickname, form.Password);
            return new ActResult<LoginResult>(StatusCode.Success, "注册成功", loginResult);
        }
    }
}
