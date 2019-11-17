using Pretty.Core.Domain.Navigations;
using Pretty.Services.Dto;
using System.Collections.Generic;

namespace Pretty.Services.Navigations
{
    public interface INavigationService
    {
        IEnumerable<Navigation> GetNavigations();

        ActResult<Navigation> InsertNavigation(Navigation nav);
    }
}
