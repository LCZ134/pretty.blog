using Pretty.Core.Chains;
using Pretty.Core.Domain.Blogs;
using System;

namespace Pretty.Plugin.CheckCmt
{
    public class IllegalHandler : ICommentHandler
    {
        public bool Execute(BlogComment comment)
        {
            if (comment.User == null)
                return false;
            return true;
        }
    }
}
