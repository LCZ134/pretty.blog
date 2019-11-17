using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pretty.Core.Helpers;
using Pretty.Services.Dto;
using Pretty.Services.Users;
using Pretty.Services.Users.Dto;
using Pretty.WebFramework.Controller;
using Pretty.WebFramework.Factories;
using Pretty.WebFramework.Filters;
using Pretty.WebFramework.Models;
using System;

namespace Pretty.Web.Controllers
{
    public class UserController : PrettyController
    {
        private IUserService _userService;
        private IUserModelFactory _userFactory;

        public UserController(
            IUserService userService, IUserModelFactory userFactory)
        {
            _userService = userService;
            _userFactory = userFactory;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public ActResult<UserDto> Get()
        {
            return _userFactory.PreparUserModel();
        }

        [HttpGet]
        [Route("api/[controller]/online")]
        public ActResult<Paged<UserDto>> GetOnlineMember(Pagingable page)
        {
            return _userFactory.PreparOnlineMember(page);
        }

        [HttpPatch]
        [Route("api/[controller]")]
        public ActResult<UserDto> Patch(UserPatch patch)
        {
            return _userFactory.PreparPatchUserModel(patch);
        }

        [HttpPost]
        [Route("api/[controller]/genVerifiyCode")]
        public bool GenVerifiyCode(string email)
        {
            var code = new Random().Next(1000, 9999).ToString();
            HttpContext.Session.SetString("loginVerifiyCode", code);
            HttpContext.Session.SetString("email", email);
            var subject = "Pretty Blog 注册验证码";
            try
            {
                EmailHelper.Send(email, subject, $"{subject}: {code}");
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        [HttpPost]
        [Route("api/[controller]/login")]
        public LoginResult Login(LoginFormModel loginForm)
        {
            return _userFactory.Login(loginForm);
        }

        [HttpPost]
        [Route("api/[controller]/register")]
        public ActResult<LoginResult> Register(RegisterFormModel form)
        {
            var code = HttpContext.Session.GetString("loginVerifiyCode");
            var email = HttpContext.Session.GetString("email");
            if (string.IsNullOrEmpty(code) || form.VerifiyCode != code || email != form.Email)
            {
                return new ActResult<LoginResult>(Services.Dto.StatusCode.Failed, "验证码不正确");
            }
            return _userFactory.Register(form);
        }

        //获取用户列表
        [HttpGet]
        [Route("api/[controller]/[action]")]
        [TypeFilter(typeof(AuthFilter), Arguments = new object[] { Policy.Admin })]
        public Paged<UserDto> GetUserList(UserPagingFilterModel form)
        {
            return _userFactory.GetUserList(form);
        }

        //加入黑名单
        [HttpPost]
        [Route("api/[controller]/{id}")]
        public ActResult<string> AddBlackList(string id)
        {
            return _userFactory.AddBlackList(id);
        }

        //删除用户


    }
}