using Pretty.Core.Domain.Blogs;
using System.Collections.Generic;

namespace Pretty.Core.Chains
{
    public class CommentHandleChain : ICommentHandler
    {
        private List<ICommentHandler> handlers = new List<ICommentHandler>();

        public CommentHandleChain AddChain(ICommentHandler handler)
        {
            handlers.Add(handler);
            return this;
        }

        public bool Execute(BlogComment comment)
        {
            foreach (var handler in handlers)
            {
                if (!handler.Execute(comment))
                    break;
            }
            return true;
        }
    }
}
