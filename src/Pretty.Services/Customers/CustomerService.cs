using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Pretty.Core.Data;
using Pretty.Core.Domain.Customers;
using Pretty.Core.Domain.Users;

namespace Pretty.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        private IRepository<Customer> _customerRepos;

        public CustomerService(IRepository<Customer> customerRepos)
        {
            _customerRepos = customerRepos;
        }

        public IEnumerable<ICustomer<TEvent>> GetAll<TEvent>()
        {
            return _customerRepos
                .Table
                .Where(i => i.EventName == typeof(TEvent).Name)
                .Include(i => i.User)
                .ToList()
                .Select(i =>
                {
                    var customer = Activator.CreateInstance(GetChildType(typeof(ICustomer<TEvent>))) as ICustomer<TEvent>;
                    customer.User = i.User;
                    return customer;
                });
        }

        public bool Subscribe<TEvent>(User user)
        {
            var eventName = typeof(TEvent).Name;
            var entity = Activator.CreateInstance<Customer>();
            entity.CreateOn = DateTime.Now;
            entity.EventName = eventName;
            entity.UserId = user.Id;
            return _customerRepos
                .Insert(entity) > 0;
        }

        private Type GetChildType(Type parentType)
        {
            Assembly assem = Assembly.GetAssembly(typeof(CustomerService));

            foreach (Type tChild in assem.GetTypes().Where(t => t.Name.EndsWith("Customer")))
            {
                if (parentType.IsAssignableFrom(tChild))
                {
                    return tChild;
                }
            }
            return null;
        }

        public bool UnSubscribe<TEvent>(User user = null)
        {
            var eventName = typeof(TEvent).Name;
            if (user == null)
            {
                return _customerRepos
                    .Delete(_customerRepos.Table.Where(i => i.EventName == eventName)) > 0;
            }
            return _customerRepos
                .Delete(_customerRepos.Table.FirstOrDefault(i => i.UserId == user.Id)) > 0;
        }
    }
}
