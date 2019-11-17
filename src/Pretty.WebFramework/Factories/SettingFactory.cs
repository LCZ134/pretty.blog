using Pretty.Core.Domain.Settings;
using Pretty.Core.Extends;
using Pretty.Services.Dto;
using Pretty.Services.Settings;
using Pretty.WebFramework.Factories.Interface;
using Pretty.WebFramework.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pretty.WebFramework.Factories
{
    public class SettingFactory : ISettingFactory
    {
        private ISettingService _settingService;

        public SettingFactory(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public ActResult<string> AddSetting(InsertSetting model)
        {
            var entity = model.Copy<Setting>();
            entity.CreateOn = DateTime.Now;
            entity.Extends = "1";
            int result = _settingService.InsertSetting(entity);
            return ActResultFactory.GetActResult(result);
        }

        public ActResult<string> Delete(string settingId)
        {
            int result = _settingService.DeleteSetting(settingId);
            return ActResultFactory.GetActResult(result);
        }

        public Paged<Setting> GetAll(SettingModel model)
        {
            return _settingService.GetAll(model.PageIndex, model.PageSize, model.Keyword);
        }

        public PagedList<Setting> GetSettings(SettingModel model)
        {
            return _settingService.GetSettings(model.PageIndex, model.PageSize, model.Keyword);
        }

        public ActResult<string> UpdateSetting(UpdateSetting model)
        {
            int result = _settingService.UpdateSetting(model.Id,model.Key,model.Value);
            return ActResultFactory.GetActResult(result);
        }
    }
}
