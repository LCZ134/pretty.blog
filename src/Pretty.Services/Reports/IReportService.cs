
using System.Collections.Generic;
using Pretty.Services.Reports.Dto;

namespace Pretty.Services.Reports
{
    public interface IReportService
    {
        WeekReport GetWeekReport();

        IEnumerable<MouthReport> GetMonthReportList(int num);
    }
}
