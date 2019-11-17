using Microsoft.AspNetCore.Mvc;
using Pretty.Core.Domain.Messages;
using Pretty.Services.Dto;
using Pretty.Services.Messages;
using Pretty.Services.Messages.Dto;
using Pretty.WebFramework.Controller;
using Pretty.WebFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pretty.Web.Controllers
{
    [Route("api/[Controller]")]
    public class MessageController : PrettyController
    {
        private IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public Paged<MessageDto> Get(MessageFilterModel filter)
        {
            return _messageService.GetAll(
                filter.PageIndex,
                filter.PageSize,
                filter.Type,
                filter.From,
                filter.To);
        }
    }
}
