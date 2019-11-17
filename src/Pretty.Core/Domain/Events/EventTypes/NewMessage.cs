using Pretty.Core.Domain.Messages;

namespace Pretty.Core.Domain.Events.EventTypes
{
    public class NewMessage
    {
        public Message Message { get; set; }
    }
}
