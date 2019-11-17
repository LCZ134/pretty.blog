using Pretty.Core.Domain.Event;
using Pretty.Services.Dto;
using System;

namespace Pretty.Services.Events
{
    public interface IEventService
    {
        void InsertEvent(Event entity);

        Paged<Event> GetEvent(DateTime? from, DateTime? to, int pageIndex, int pageSize,bool isALL=false);

    }
}
