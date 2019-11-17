using Pretty.Services.Blogs;
using Pretty.Services.Reports;
using Pretty.Services.Reports.Dto;
using Pretty.WebFramework.Factories.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pretty.WebFramework.Factories
{
    class ReportModelFactory : IReportModelFactory
    {
        private IReportService _reportService;

       public ReportModelFactory(IReportService reportService)
        {
            _reportService = reportService;
        }

        public IEnumerable<MouthReport> GetMonthReportList(int num=4)
        {
            return _reportService.GetMonthReportList(num);
        }

        public WeekReport GetWeekReport()
        {
            return _reportService.GetWeekReport();
        }
    }
}
