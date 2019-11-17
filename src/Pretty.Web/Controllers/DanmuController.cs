using Microsoft.AspNetCore.Mvc;
using Pretty.Services.Danmus.Dto;
using Pretty.Services.Dto;
using Pretty.WebFramework.Controller;
using Pretty.WebFramework.Factories;
using Pretty.WebFramework.Filters;
using Pretty.WebFramework.Models;

namespace Pretty.Web.Controllers
{
    public class DanmuController : PrettyController
    {
        private IDanmuFactory _danmuFactory;

        public DanmuController(IDanmuFactory danmuFactory)
        {
            _danmuFactory = danmuFactory;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public Paged<DanmuDto> Get(DanmuPagingModel command)
        {
            return _danmuFactory.FetchAllDanmu(command);
        }
    
        [HttpPost]
        [Route("api/[controller]")]
        [TypeFilter(typeof(AuthFilter), Arguments = new object[] { Policy.Logged })]
        public ActResult<string> Post(InsertDanmuModel danmu)
        {
            return _danmuFactory.AddDanmu(danmu);
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        [TypeFilter(typeof(AuthFilter), Arguments = new object[] { Policy.Admin })]
        public ActResult<string> Delete(string id)
        {
            return _danmuFactory.DeleteById(id);
        }

        [HttpDelete]
        [Route("api/[controller]/deletebywordKey")]
        [TypeFilter(typeof(AuthFilter), Arguments = new object[] { Policy.Admin })]
        public ActResult<string> DeleteByWordKey(string context)
        {
            return _danmuFactory.DeleteByWordKey(context);
        }
    }
}
