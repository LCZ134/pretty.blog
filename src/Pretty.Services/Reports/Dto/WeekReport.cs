namespace Pretty.Services.Reports.Dto
{
    public class WeekReport
    {
        //浏览
        public int ViewCount { get; set; }

        //点赞
        public int ThumbUpCount { get; set; }
            
        //发布
        public int PublishCount{ get; set; }

        //回复评论
        public int ReplyCount { get; set; }
    }
}
