using Microsoft.AspNetCore.Mvc;
using Pretty.Core.Domain.Event;
using Pretty.Core.Domain.Events;
using Pretty.Services.Blogs.Dto;
using Pretty.Services.Dto;
using Pretty.Services.Events;
using Pretty.WebFramework.Controller;
using Pretty.WebFramework.Factories;
using Pretty.WebFramework.Factories.Interface;
using Pretty.WebFramework.Filters;
using Pretty.WebFramework.Models;
using Pretty.WebFramework.Models.Events;

namespace Pretty.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    public class BrowsingHistoryController : PrettyController
    {
        private IBrowsingHistoryFactory _browingHistoryFactory;
        private IEventFactory _eventFactory;

        public BrowsingHistoryController(IBrowsingHistoryFactory browingHistoryFactory, IEventFactory eventFactory)
        {
            _browingHistoryFactory = browingHistoryFactory;
            _eventFactory = eventFactory;
        }

        [HttpGet]
        [TypeFilter(typeof(AuthFilter), Arguments = new object[] { Policy.Logged })]
        public ActResult<Paged<BlogPostDto>> Get(HistoryPagingFilter filter)
        {
            return _browingHistoryFactory.PrepareHistoryModel(filter);
        }

        [HttpPost]
        [TypeFilter(typeof(AuthFilter), Arguments = new object[] { Policy.Logged })]
        public bool Post(string blogPostId)
        {
            return _browingHistoryFactory.InsertHistory(blogPostId);
        }

        [HttpGet]
        [TypeFilter(typeof(AuthFilter), Arguments = new object[] { Policy.Admin })]
        public Paged<Event> GetEvent(EventModel model)
        {
            return _eventFactory.GetEvent(model);
        }

    }
}
