using Microsoft.AspNetCore.Mvc;
using Pretty.Core;
using Pretty.Core.Domain.Events.EventTypes;
using Pretty.Services.Customers;
using Pretty.WebFramework.Controller;
using Pretty.WebFramework.Filters;

namespace Pretty.Web.Controllers
{
    public class CustomerController : PrettyController
    {
        private ICustomerService _customerService;
        private IWorkContext _workContext;

        public CustomerController(ICustomerService customerService, IWorkContext workContext)
        {
            _customerService = customerService;
            _workContext = workContext;
        }

        [HttpPost]
        [Route("api/subscribe")]
        [TypeFilter(typeof(AuthFilter), Arguments = new object[] { Policy.Logged })]
        public bool Subscribe()
        {
            return _customerService.Subscribe<PublishBlog>(_workContext.User);
        }

        [HttpPost]
        [Route("api/unSubscribe")]
        [TypeFilter(typeof(AuthFilter), Arguments = new object[] { Policy.Logged })]
        public bool UnSubscribe()
        {
            return _customerService.UnSubscribe<PublishBlog>(_workContext.User);
        }
    }
}
