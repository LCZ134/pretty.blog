using System;
using Pretty.Core.Domain.Events.EventTypes;

namespace Pretty.Services.Events
{
    public interface IEventPublisher
    {
        void Publish<TEvent>(TEvent @event);

        void On<TEvent>(Action<TEvent> action);
    }
}
