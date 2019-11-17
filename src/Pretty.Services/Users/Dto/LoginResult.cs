namespace Pretty.Services.Users.Dto
{
    public enum LoginStatus
    {
        Success = 0,
        PasswordError = 1,
        UserNotExists = 2
    }

    public class LoginResult
    {
        /// <summary>
        /// login status code
        /// </summary>
        public LoginStatus StatusCode { get; set; }

        /// <summary>
        /// login user
        /// </summary>
        public UserDto User { get; set; }

        /// <summary>
        /// login token
        /// </summary>
        public string Token { get; set; }
    }
}
