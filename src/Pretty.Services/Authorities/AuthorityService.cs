using System;
using System.Collections.Generic;
using System.Linq;
using Pretty.Core;
using Pretty.Core.Data;
using Pretty.Core.Domain.Authorities;
using Pretty.Core.Domain.Users;
using Pretty.Data;
using Pretty.Services.Users;

namespace Pretty.Services.Authorities
{
    public class AuthorityService : IAuthorityService
    {
        private IRepository<Authority> _authorityRepository;

        public AuthorityService(IRepository<Authority> authorityRepository)
        {
            _authorityRepository = authorityRepository;
        }


        /// <summary>
        /// authorize user
        /// </summary>
        /// <param name="user">user</param>
        /// <param name="Exprie">exprie date</param>
        /// <returns>token</returns>
        public string AuthorizeUser(User user, DateTime? Exprie)
        {
            if (user == null)
                throw new ArgumentNullException("user is null");

            var token = Utils.NewGuid();
            var entity = new Authority()
            {
                CreateOn = DateTime.Now,
                Expire = Exprie,
                Token = token,
                UserId = user.Id
            };

            _authorityRepository.Insert(entity);

            return token;
        }

        public string GetUserIdByToken(string token)
        {
            var authority = _authorityRepository
                .Table
                .Where(i => i.Token == token && i.Expire.HasValue && i.Expire.Value >= DateTime.Now)
                .FirstOrDefault();

            return authority?.UserId;
        }
    }
}
