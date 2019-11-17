using Pretty.Core.Domain.Users;
using System;

namespace Pretty.Services.Authorities
{
    public interface IAuthorityService
    {

        /// <summary>
        /// authorize user
        /// </summary>
        /// <param name="user">user</param>
        /// <param name="Exprie">exprie date</param>
        /// <returns>token</returns>
        string AuthorizeUser(User user, DateTime? Exprie);

        /// <summary>
        /// get user identity by token
        /// </summary>
        /// <param name="token">token</param>
        /// <returns>user identity</returns>
        string GetUserIdByToken(string token);
    }
}
