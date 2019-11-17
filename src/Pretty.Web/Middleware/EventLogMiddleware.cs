using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Pretty.Core;
using Pretty.Services.Events;

namespace Pretty.Web.Middleware
{
    public class EventLogMiddleware : IMiddleware
    {
        private IEventService _eventServcie;
        private IWorkContext _workContext;

        public EventLogMiddleware(IEventService eventServcie, IWorkContext workContext)
        {
            _eventServcie = eventServcie;
            _workContext = workContext;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _eventServcie.InsertEvent(new Core.Domain.Event.Event
            {
                RequestUrl = context.Request.Path.Value,
                ReferrerUrl = context.Request.Headers["User-Agent"],
                UserId = _workContext.User?.Id,
                IpAddress = context.Request.Host.Host,
                CreateOn = DateTime.Now
            });            
            await next.Invoke(context);

        }
    }
}
