using Pretty.Core;
using System;
using System.Collections.Generic;

namespace Pretty.Services.Events
{
    public class EventStore<TEvent>
    {
        private List<Action<TEvent>> _events = new List<Action<TEvent>>();

        public static EventStore<TEvent> Of()
        {
            Singletion.Register<EventStore<TEvent>>();
            return Singletion.Get<EventStore<TEvent>>();
        }

        public void Add(Action<TEvent> action)
        {
            try
            {
                _events.Add(action);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public void InvokeAll(TEvent @event)
        {
            _events.ForEach(i => i?.Invoke(@event));
        }
    }
}
