using Pretty.Core.Domain.Event;
using Pretty.Services.Dto;
using Pretty.Services.Events;
using Pretty.WebFramework.Factories.Interface;
using Pretty.WebFramework.Models.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pretty.WebFramework.Factories
{
    public class EventFactory : IEventFactory
    {
        private IEventService  _eventService;

        public EventFactory(IEventService eventService)
        {
            _eventService = eventService;
        }

        public Paged<Event> GetEvent(EventModel model)
        {
            return _eventService.GetEvent(model.DateFrom, model.DateTo,model.PageIndex, model.PageSize, model.IsALL);
        }
    }
}
