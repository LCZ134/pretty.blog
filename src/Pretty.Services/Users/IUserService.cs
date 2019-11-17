using System;
using System.Linq.Expressions;
using Pretty.Core.Domain.Users;
using Pretty.Services.Dto;
using Pretty.Services.Users.Dto;

namespace Pretty.Services.Users
{
    public interface IUserService
    {
        User GetUserById(string id);

        LoginResult Login(string nickname, string password);

        User Login(string nickname, string password,out LoginStatus statusCode);

        User Register(UserDto user, string pwd);

        bool UpdateUser(User entity, string[] props);

        User GetUserByNickname(string nickname);

        User GetUserByEmail(string email);

        int UpdateBlackList(string UserId, bool state);

        Paged<UserDto> GetAllUser(
            string name, int pageIndex, 
            int pageSize, DateTime? dateFrom,
            DateTime? dateTo, Expression<Func<User, bool>> whereExpr = null);
    }
}
