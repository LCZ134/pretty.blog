using Microsoft.AspNetCore.SignalR;
using Pretty.Core;
using Pretty.Core.Domain.Customers;
using Pretty.Core.Domain.Events.EventTypes;
using Pretty.Core.Domain.Users;
using Pretty.Core.Extends;
using Pretty.Services.Events;
using Pretty.Services.Messages;
using Pretty.Services.Users;
using Pretty.Services.Users.Dto;
using Pretty.WebFramework.Models;
using System;
using System.Threading.Tasks;

namespace Pretty.WebFramework.Hubs
{
    public class ChatHub : Hub
    {
        private IWorkContext _workContext;
        private IMessageService _messageService;
        private IEventPublisher _eventPublisher;
        private IUserService _userService;

        public ChatHub(
            IWorkContext workContext,
            IMessageService messageService,
            IEventPublisher eventPublisher,
            IUserService userService)
        {
            _workContext = workContext;
            _messageService = messageService;
            _eventPublisher = eventPublisher;
            _userService = userService;
        }

        private async Task OnNewMessage<TEvent>(User user, string message, DateTime date)
        {
            var id = _messageService.InsertMessage<TEvent>(message, user.Id);
            await Clients.All.SendAsync("messageReceived", new ChatMessage<string>
           (
                id,
               user?.Copy<UserDto>(),
               message,
               typeof(TEvent).Name,
               date
           ));
        }

        public async Task NewMessage(string message)
        {
            var user = _workContext.User;
            if (user == null)
                return;
            await OnNewMessage<NewMessage>(user, message, DateTime.Now);
        }


        public async override Task OnConnectedAsync()
        {
            var user = _workContext.User;
            if (user != null)
            {
                 user.Online = 1;
                _userService.UpdateUser(user, new string[] { "Online" });
                await OnNewMessage<Connect>(user, "CONNECT", DateTime.Now);
            }

            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            var user = _workContext.User;
            if (user != null)
            {
                user.Online = 0;
                _userService.UpdateUser(user, new string[] { "Online" });
                await OnNewMessage<DisConnect>(user, "DISCONNECT", DateTime.Now);
            }


            await base.OnDisconnectedAsync(exception);
        }
    }
}
