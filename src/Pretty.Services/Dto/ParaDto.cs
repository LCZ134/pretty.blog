using System;
using System.Collections.Generic;
using System.Text;

namespace Pretty.Services.Dto
{
    public  class ParaDto
    {
        //全部文章数
        public int BlogPostCount { get; set; }
        //全部标签数
        public int BlogTagCount { get; set; }
        //全部评论数
        public int BlogCommentCount { get; set; }
        //今日阅读数
        public int TodayReadCount { get; set; }
    }
}
