using Pretty.Services.Dto;
using Pretty.Services.Users.Dto;
using System;

namespace Pretty.Services.Messages.Dto
{
    public class MessageDto : BaseDto
    {
        public string Id { get; set; }

        public string Type { get; set; }

        public string Content { get; set; }

        public UserDto User { get; set; }

        public DateTime CreateOn { get; set; }
    }
}
