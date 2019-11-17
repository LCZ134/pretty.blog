using Pretty.Services.Reports.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pretty.WebFramework.Factories.Interface
{
    public  interface IReportModelFactory
    {
         WeekReport GetWeekReport();
        IEnumerable<MouthReport> GetMonthReportList(int num);
    }
}
