using Pretty.Core.Data;
using Pretty.Core.Domain;
using Pretty.Core.Domain.Blogs;
using Pretty.Core.Domain.Events;
using Pretty.Services.Reports.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pretty.Services.Reports
{
    public class ReportService : IReportService
    {
        private IRepository<BlogPost> _blogPostRepos;
        private IRepository<ThumbsUp> _thumbUpRepos;
        private IRepository<BrowsingHistory> _historyRepos;
        private IRepository<BlogComment> _commentRepos;

        public ReportService(
            IRepository<BlogPost> blogPostRepos,
            IRepository<ThumbsUp> thumbUpRepos,
            IRepository<BrowsingHistory> historyRepos,
            IRepository<BlogComment> commentRepos
            )
        {
            _blogPostRepos = blogPostRepos;
            _thumbUpRepos = thumbUpRepos;
            _historyRepos = historyRepos;
            _commentRepos = commentRepos;
        }

        public IEnumerable<MouthReport> GetMonthReportList(int num)
        {
            var result = new List<MouthReport>();

            for (int i = 0; i < num; i++)
            {
                result.Add(new MouthReport()
                {
                    Month = DateTime.Now.Month-i,
                    PublishCount = GetMonth(_blogPostRepos, -i),
                    ReplyCount = GetMonth(_commentRepos, -i),
                    ThumbUpCount = GetMonth(_thumbUpRepos, -i),
                    ViewCount = GetMonth(_historyRepos, -i),
                });
            }
            return result;
        }


        public int GetMonth<TEntity>(IRepository<TEntity> repos,int num) where TEntity : BaseEntity
        {
            var query = repos.Table;
            var stateMouth = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(-num);//月初
            var endMouth=DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(num+1).AddDays(-1);//月末
            return  query.Where(i => i.CreateOn <= endMouth && i.CreateOn >= stateMouth).Count();
        }

        public WeekReport GetWeekReport()
        {
            return new WeekReport
            {
                PublishCount = GetCurWeekInsertCount(_blogPostRepos),
                ReplyCount = GetCurWeekInsertCount(_commentRepos),
                ThumbUpCount = GetCurWeekInsertCount(_thumbUpRepos),
                ViewCount = GetCurWeekInsertCount(_historyRepos)
            };
        }

        private int GetCurWeekInsertCount<TEntity>(IRepository<TEntity> repos) where TEntity : BaseEntity
        {
            var curWeekFirstDay = DateTime.Now.AddDays(0 - Convert.ToInt16(DateTime.Now.DayOfWeek));
            return repos.Table.Where(i => i.CreateOn >= curWeekFirstDay).Count();
        }

    }
}
