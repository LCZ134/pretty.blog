using Pretty.Core.Domain.Roles;
using Pretty.Data.Mapping.Role.Dto;
using Pretty.Services.Dto;

namespace Pretty.Services.Users.Dto
{
    public class UserDto : BaseDto
    {
        public UserDto()
        {
            AvatarUrl = "./assets/img/faces/avatar.jpg";
        }
        public string Id { get; set; }

        public string Email { get; set; }

        public string NickName { get; set; }

        public string AvatarUrl { get; set; }

        public virtual RoleDto Role { get; set; }

        public int Gender { get; set; }
    }
}
