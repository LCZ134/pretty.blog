using Pretty.Core;
using Pretty.Services.Users.Dto;
using System;

namespace Pretty.WebFramework.Models
{

    public class ChatMessage<T>
    {
        public ChatMessage(string id, UserDto user, T message, string type, DateTime createOn)
        {
            User = user;
            Content = message;
            Type = type;
            CreateOn = createOn;
            Id = id;
        }

        public string Id { get; set; }

        public UserDto User { get; set; }

        public T Content { get; set; }

        public string Type { get; set; }

        public DateTime CreateOn { get; set; }
    }
}
