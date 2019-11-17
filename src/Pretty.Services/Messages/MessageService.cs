using Microsoft.EntityFrameworkCore;
using Pretty.Core;
using Pretty.Core.Data;
using Pretty.Core.Domain.Events.EventTypes;
using Pretty.Core.Domain.Messages;
using Pretty.Services.Dto;
using Pretty.Services.Events;
using Pretty.Services.Extends;
using Pretty.Services.Messages.Dto;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Pretty.Services.Messages
{
    public class MessageService : IMessageService
    {
        private IRepository<Message> _messageRepos;
        private IEventPublisher _eventPublisher;

        public MessageService(IRepository<Message> messageRepos, IEventPublisher eventPublisher)
        {
            _messageRepos = messageRepos;
            _eventPublisher = eventPublisher;
        }

        public Paged<MessageDto> GetAll(int pageIndex, int pageSize, string type, DateTime? from, DateTime? to)
        {
            var query = _messageRepos.Table;

            if (from.HasValue)
                query = query.Where(i => from.Value <= i.CreateOn);
            if (to.HasValue)
                query = query.Where(i => to.Value >= i.CreateOn);
            if (!string.IsNullOrEmpty(type))
                query = query.Where(i => EF.Functions.Like(i.Type, $"%{type}%"));
            query = query.OrderByDescending(i => i.CreateOn);

            var blogPosts = new PagedList<Message>(
                query.Include(i => i.User),
                pageIndex,
                pageSize
                );

            return blogPosts.GetDto<Message, MessageDto>();
        }

        private void MessageDto()
        {
            throw new NotImplementedException();
        }

        public string InsertMessage<TEvent>(string content, string userId)
        {
            var message = new Message()
            {
                Content = content,
                UserId = userId,
                Type = typeof(TEvent).Name,
                CreateOn = DateTime.Now
            };

            _eventPublisher.Publish(new NewMessage() { Message = message });
            return _messageRepos.Insert(message) > 0 ? message.Id : null;
        }
    }
}
