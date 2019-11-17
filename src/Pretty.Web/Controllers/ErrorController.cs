using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pretty.Services.Blogs.Dto;
using Pretty.Services.Dto;

namespace Pretty.Web.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet]
        [Route("api/[controller]/unAuthorize")]
        public JsonResult UnAuthorize()
        {
            return Json(new ActResult<string>(Services.Dto.StatusCode.Failed, "权限不足."));
        }
    }
}