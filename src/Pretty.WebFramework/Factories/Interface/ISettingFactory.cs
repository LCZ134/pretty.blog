using Pretty.Core.Domain.Settings;
using Pretty.Services.Dto;
using Pretty.WebFramework.Models;

namespace Pretty.WebFramework.Factories.Interface
{
    public interface ISettingFactory
    {

        Paged<Setting> GetAll(SettingModel model);

        PagedList<Setting> GetSettings(SettingModel model);

        ActResult<string> Delete(string settingId);

        ActResult<string> AddSetting(InsertSetting model);

        ActResult<string> UpdateSetting(UpdateSetting model);
    }
}
