using Microsoft.AspNetCore.Mvc;
using Pretty.Core.Domain.Navigations;
using Pretty.Services.Dto;
using Pretty.Services.Navigations;
using Pretty.WebFramework.Controller;
using System.Collections.Generic;

namespace Pretty.Web.Controllers
{
    [Route("api/[controller]")]
    public class NavigationController : PrettyController
    {
        private INavigationService _navServcie;

        public NavigationController(
            INavigationService navServcie)
        {
            _navServcie = navServcie;
        }

        [HttpGet]
        public IEnumerable<Navigation> Get()
        {
            return _navServcie.GetNavigations();
        }

        [HttpPost]
        public ActResult<Navigation> Post(Navigation nav)
        {
            return _navServcie.InsertNavigation(nav);
        }
    }
}