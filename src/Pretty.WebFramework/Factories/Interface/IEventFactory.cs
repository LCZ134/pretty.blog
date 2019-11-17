using Pretty.Core.Domain.Event;
using Pretty.Services.Dto;
using Pretty.WebFramework.Models.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pretty.WebFramework.Factories.Interface
{
    public interface  IEventFactory
    {
        Paged<Event> GetEvent(EventModel model);
    }
}
