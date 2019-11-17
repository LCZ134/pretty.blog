using Pretty.Core.Domain.Users;
using System.Threading.Tasks;

namespace Pretty.Core.Domain.Customers
{
    public interface ICustomer<TEvent>
    {
        User User { get; set; }

        void HandleEvent(TEvent @event);

        Task HandleEventAsync(TEvent @event);
    }
}
