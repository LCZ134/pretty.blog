using Microsoft.EntityFrameworkCore;
using Pretty.Core.Data;
using Pretty.Core.Domain.Users;
using Pretty.Core.Extends;
using Pretty.Services.Authorities;
using Pretty.Services.Dto;
using Pretty.Services.Extends;
using Pretty.Services.Securities;
using Pretty.Services.Users.Dto;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Pretty.Services.Users
{
    public class UserService : IUserService
    {
        private IRepository<User> _userRepository;
        private IEncryptionService _encryptionService;
        private IAuthorityService _authorityService;

        public UserService(
            IRepository<User> userRepository,
            IEncryptionService encryptionService,
            IAuthorityService authorityService)
        {
            _userRepository = userRepository;
            _encryptionService = encryptionService;
            _authorityService = authorityService;
        }

        public Paged<UserDto> GetAllUser(
            string name, int pageIndex, 
            int pageSize, DateTime? dateFrom, 
            DateTime? dateTo, Expression<Func<User, bool>> whereExpr = null)
        {
            pageSize = pageSize <= 0 ? 10 : pageSize;
            var query = _userRepository.Table;

            if (!string.IsNullOrEmpty(name))
                query = query.Where(i => i.NickName == name);
            if (dateFrom.HasValue)
                query = query.Where(i => dateFrom.Value <= i.CreateOn);
            if (dateTo.HasValue)
                query = query.Where(i => dateTo.Value >= i.CreateOn);
            if (whereExpr != null)
                query = query.Where(whereExpr);

            var incQuery = query.Include(i => i.Role);

            var users = new PagedList<User>(
                incQuery,
                pageIndex,
                pageSize
            );

            var result = users.GetDto<User, UserDto>();
            return result;
        }

        public User GetUserByEmail(string email)
        {
            return _userRepository.Table
                .FirstOrDefault(i => i.Email == email);
        }

        public User GetUserById(string id)
        {
            return _userRepository.GetById(id);
        }

        public User GetUserByNickname(string nickname)
        {
            return _userRepository.Table
                .FirstOrDefault(i => i.NickName == nickname);
        }

        public LoginResult Login(string email, string password)
        {
            var statusCode = LoginStatus.Success;
            string token = string.Empty;
            var user = _userRepository.Table.Where(i => i.Email == email).FirstOrDefault();
            if (user == null)
                statusCode = LoginStatus.UserNotExists;
            else if (!PasswordsMatch(user, password))
                statusCode = LoginStatus.PasswordError;
            else
                token = _authorityService.AuthorizeUser(user, DateTime.Now.AddDays(30));
            return new LoginResult
            {
                StatusCode = statusCode,
                User = user?.Copy<UserDto>(),
                Token = token
            };
        }

        public User Login(string email, string password,out LoginStatus statusCode)
        {
            statusCode = LoginStatus.Success;
            User user= _userRepository.Table.Where(i => i.Email == email).FirstOrDefault();
            if (user == null)
                statusCode = LoginStatus.UserNotExists;
            else if (!PasswordsMatch(user, password))
                statusCode = LoginStatus.PasswordError;
            return user;
        }



        public User Register(UserDto user, string password)
        {
            var slat = _encryptionService.CreateSaltKey(6);

            var entity = user.Copy<User>();
            entity.Salt = slat;
            entity.CreateOn = DateTime.Now;
            entity.Password = _encryptionService.CreatePasswordHash(password, slat);

            _userRepository.Insert(entity);
            return entity;
        }

        public int UpdateBlackList(string UserId, bool state)
        {
            short isBlackList = (short)(state ? 0 : 1);
            var user = _userRepository.Table.FirstOrDefault(i => i.Id == UserId);
            if (user == null)
                return 0;
            //还未添加列，还要添加一个黑名单表记录，因为什么原因加入黑名单
            
            return 1;
        }

        public bool UpdateUser(User entity, string[] updateProps)
        {
            return _userRepository.Update(entity, updateProps) > 0;
        }

        #region Utilities

        /// <summary>
        /// Check whether the entered password matches with a saved one
        /// </summary>
        /// <param name="userPassword">User password</param>
        /// <param name="enteredPassword">The entered password</param>
        /// <returns>True if passwords match; otherwise false</returns>
        protected bool PasswordsMatch(User user, string enteredPassword)
        {
            if (user == null || string.IsNullOrEmpty(enteredPassword))
                return false;
            string savedPassword = _encryptionService.CreatePasswordHash(enteredPassword, user.Salt);

            if (user.Password == null)
                return false;

            return user.Password.Equals(savedPassword);
        }

        #endregion
    }
}

