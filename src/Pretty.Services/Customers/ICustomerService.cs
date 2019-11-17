using Pretty.Core.Domain.Customers;
using Pretty.Core.Domain.Users;
using System.Collections.Generic;

namespace Pretty.Services.Customers
{
    public interface ICustomerService
    {
        bool Subscribe<TEvent>(User user);

        bool UnSubscribe<TEvent>(User user);

        IEnumerable<ICustomer<TEvent>> GetAll<TEvent>();
    }
}
