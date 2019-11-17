using Microsoft.EntityFrameworkCore;
using Pretty.Core.Data;
using Pretty.Core.Domain.Event;
using Pretty.Data;
using Pretty.Services.Dto;
using Pretty.Services.Extends;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pretty.Services.Events
{
    public class EventService : IEventService
    {
        private IRepository<Event> _eventRepository;
        private IList<Event> _events = new List<Event>();

        public EventService(IRepository<Event> eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public Paged<Event> GetEvent(DateTime? from, DateTime? to, int pageIndex, int pageSize,bool isALL=false)
        {
            var query = _eventRepository.Table;
            if (from.HasValue)
                query = query.Where(i => from.Value <= i.CreateOn);
            if (to.HasValue)
                query = query.Where(i => to.Value >= i.CreateOn);
            if (!isALL)
                query = query.Where(i => !string.IsNullOrEmpty(i.UserId)&&i.RequestUrl == "/api/user/login");
            var incQuery = query.Include(i => i.User);
            var danmu = new PagedList<Event>(incQuery, pageIndex, pageSize);
            var result = danmu.GetDto();
            return result;
        }

        public void InsertEvent(Event entity)
        {
            lock (_events)
            {
                _events.Add(entity);
                if (_events.Count > 10)
                {
                    _eventRepository.Insert(_events);
                    _events.Clear();
                }
            }
        }
    }
}
