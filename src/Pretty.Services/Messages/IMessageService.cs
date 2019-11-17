using Pretty.Core.Domain.Messages;
using Pretty.Services.Dto;
using Pretty.Services.Messages.Dto;
using System;
using System.Threading.Tasks;

namespace Pretty.Services.Messages
{
    public interface IMessageService
    {
        string InsertMessage<TEvent>(string content, string userId);

        Paged<MessageDto> GetAll(int pageIndex, int pageSize, string type, DateTime? from, DateTime? to);
    }
}
