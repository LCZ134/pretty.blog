using Microsoft.AspNetCore.Mvc;
using Pretty.Core.Domain.Settings;
using Pretty.Services.Dto;
using Pretty.Services.Settings;
using Pretty.WebFramework.Controller;
using Pretty.WebFramework.Factories.Interface;
using Pretty.WebFramework.Filters;
using Pretty.WebFramework.Models;
using static Pretty.WebFramework.Filters.AuthFilter;

namespace Pretty.Web.Controllers
{
    public class SettingController : PrettyController
    {
        private ISettingService _settingService;

        private ISettingFactory _sttingFactory;

        public SettingController(ISettingService settingService, ISettingFactory sttingFacroty)
        {
            _settingService = settingService;
            _sttingFactory = sttingFacroty;
        }

        [HttpGet]
        [Route("api/[controller]/{key}")]
        public ActResult<Setting> Get(string key)
        {
            return new ActResult<Setting>(Services.Dto.StatusCode.Success, "获取信息成功")
            { Extends = _settingService.Get(key) };
        }

        [HttpPut]
        [Route("api/[controller]")]
        [TypeFilter(typeof(AuthFilter), Arguments = new object[] { Policy.Admin })]
        public ActResult<Setting> Put(string key, string value, string extends)
        {
            return new ActResult<Setting>(
                Services.Dto.StatusCode.Success, "修改成功")
            { Extends = _settingService.Set(key, value, extends) };
        }

        [HttpGet]
        [Route("api/[controller]/[action]")]
        [TypeFilter(typeof(AuthFilter), Arguments = new object[] { Policy.Admin })]
        public Paged<Setting> GetAll(SettingModel model)
        {
            return _sttingFactory.GetAll(model);
        }

        [HttpDelete]
        [Route("api/[controller]")]
        [TypeFilter(typeof(AuthFilter), Arguments = new object[] { Policy.Admin })]
        public ActResult<string> Delete(string id)
        {
            return _sttingFactory.Delete(id);
        }

        [HttpPost]
        [Route("api/[controller]")]
        [TypeFilter(typeof(AuthFilter), Arguments = new object[] { Policy.Admin })]
        public ActResult<string> HttpPost(InsertSetting model)
        {
            return _sttingFactory.AddSetting(model);
        }

        [HttpPatch]
        [Route("api/[controller]")]
        [TypeFilter(typeof(AuthFilter), Arguments = new object[] { Policy.Admin })]
        public ActResult<string> HttpPatch(UpdateSetting model)
        {
            return _sttingFactory.UpdateSetting(model);
        }

    }
}