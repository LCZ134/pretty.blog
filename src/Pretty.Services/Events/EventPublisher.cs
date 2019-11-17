using Pretty.Core.Domain.Customers;
using Pretty.Core.Domain.Events.EventTypes;
using Pretty.Services.Customers;
using System;
using System.Linq;

namespace Pretty.Services.Events
{
    public class EventPublisher : IEventPublisher
    {
        private ICustomerService _customerService;

        public EventPublisher(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public void Publish<TEvent>(TEvent @event)
        {
            var customers = _customerService.GetAll<TEvent>();
            foreach (var customer in customers)
            {
                customer.HandleEventAsync(@event);
            }

            EventStore<TEvent>.Of().InvokeAll(@event);
        }

        public void On<TEvent>(Action<TEvent> action)
        {
            EventStore<TEvent>.Of().Add(action);
        }
    }
}
