using Microsoft.AspNetCore.Mvc;
using Pretty.Data;
using Pretty.Services.Reports.Dto;
using Pretty.WebFramework.Controller;
using Pretty.WebFramework.Factories.Interface;
using System.Collections.Generic;

namespace Pretty.Web.Controllers
{
    public class ReportController : PrettyController
    {

        private IReportModelFactory _reportModel;

        public ReportController(IReportModelFactory reportModel )
        {
            _reportModel = reportModel;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public WeekReport Get()
        {
            return _reportModel.GetWeekReport();
        }

        //获取每月的数据
        [HttpGet]
        [Route("api/[controller]/[action]")]
        public IEnumerable<MouthReport>  GetMonth()
        {
            return _reportModel.GetMonthReportList(4);
        }

    }
}