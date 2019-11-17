using Pretty.Core.Domain.Users;
using Pretty.Services.Dto;
using Pretty.Services.Users.Dto;
using Pretty.WebFramework.Models;

namespace Pretty.WebFramework.Factories
{
    public interface IUserModelFactory
    {
        ActResult<UserDto> PreparUserModel();

        ActResult<LoginResult> Register(RegisterFormModel form);

        LoginResult Login(LoginFormModel loginForm);

        ActResult<UserDto> PreparPatchUserModel(UserPatch patch);

        ActResult<string> AddBlackList(string id);

        Paged<UserDto> GetUserList(UserPagingFilterModel form);

        ActResult<Paged<UserDto>> PreparOnlineMember(Pagingable page);
    }
}
