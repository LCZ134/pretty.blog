using Pretty.Core.Domain.Customers;
using Pretty.Core.Domain.Events.EventTypes;
using Pretty.Core.Domain.Users;
using Pretty.Core.Helpers;

namespace Pretty.Services.Customers
{
    public class PublishBlogCustomer : CustomerBase<PublishBlog>
    {
        public override void HandleEvent(PublishBlog @event)
        {
            EmailHelper.Send("1029174296@qq.com",
                $"您订阅的博客有新内容了。", $"user:{User.NickName}," +
                $" title: {@event.Entity.Title}");
        }
    }
}
