using Pretty.Core;
using Pretty.Core.Chains;
using Pretty.Core.Domain.Blogs;
using System;

namespace Pretty.Plugin.CheckCmt
{
    public class SensitiveWordHandler : ICommentHandler
    {
        private readonly string[] coreSocialismValues = new string[]
        {"富强","民主","文明","和谐","自由" ,"平等","公正","法治","爱国","敬业","诚信","友善"};

        public bool Execute(BlogComment comment)
        {

            comment.Content = SensitivewordEngine.Match(comment.Content, words =>
            {
                return $"({coreSocialismValues[Utils.GetRandomNum(0, 11)]})";
            });

            return true;
        }
    }
}
