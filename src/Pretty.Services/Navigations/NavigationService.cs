using System.Collections.Generic;
using Pretty.Core.Data;
using Pretty.Core.Domain.Navigations;
using Pretty.Services.Dto;

namespace Pretty.Services.Navigations
{
    public class NavigationService : INavigationService
    {
        private IRepository<Navigation> _navRepository;

        public NavigationService(IRepository<Navigation> navRepository)
        {
            _navRepository = navRepository;
        }

        public IEnumerable<Navigation> GetNavigations()
        {
            return _navRepository.Table;
        }

        public ActResult<Navigation> InsertNavigation(Navigation nav)
        {
            try
            {
                _navRepository.Insert(nav);
                return new ActResult<Navigation>(StatusCode.Success, "添加成功") { Extends = nav };
            }
            catch (System.Exception e)
            {
                return new ActResult<Navigation>(StatusCode.Success, e.Message);
            }

        }

    }
}
