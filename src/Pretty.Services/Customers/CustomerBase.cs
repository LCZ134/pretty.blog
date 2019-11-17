using Pretty.Core.Domain.Customers;
using Pretty.Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pretty.Services.Customers
{
    public abstract class CustomerBase<TEvent> : ICustomer<TEvent>
    {
        public User User { get; set; }

        public abstract void HandleEvent(TEvent @event);

        public Task HandleEventAsync(TEvent @event)
        {
            return Task.Run(() => HandleEvent(@event));
        }
    }
}
