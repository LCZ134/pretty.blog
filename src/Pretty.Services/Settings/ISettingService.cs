using Pretty.Core.Domain.Settings;
using Pretty.Services.Dto;

namespace Pretty.Services.Settings
{
    public interface ISettingService
    {
        Setting Get(string key);

        string GetValue(string key);

        Setting Set(string key, object value, object extends);

        PagedList<Setting> GetSettings(
            int pageIndex,
            int pageSize,
            string keyword);

        Paged<Setting> GetAll(
            int pageIndex,
            int pageSize,
            string keyword);

        int DeleteSetting(string settingId);

        int InsertSetting(Setting model);

        int UpdateSetting(string id, string key, string value);
    }
}
