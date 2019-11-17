using Pretty.Core.Domain.Users;

namespace Pretty.Core
{
    public interface IWorkContext
    {
        User User { get; }
    }
}
