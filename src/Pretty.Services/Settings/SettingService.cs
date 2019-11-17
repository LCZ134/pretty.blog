using System;
using System.Linq;
using Pretty.Core.Data;
using Pretty.Core.Domain.Settings;
using Pretty.Services.Dto;
using Pretty.Services.Extends;

namespace Pretty.Services.Settings
{
    public class SettingService : ISettingService
    {
        private IRepository<Setting> _settingRepository;

        public SettingService(IRepository<Setting> settingRepository)
        {
            _settingRepository = settingRepository;
        }

        public int DeleteSetting(string settingId)
        {
            return _settingRepository.Delete(_settingRepository.GetById(settingId));
        }

        public Setting Get(string key)
        {
            var query = _settingRepository.Table;

            return query.Where(i => i.Key == key).FirstOrDefault();
        }

        public Paged<Setting> GetAll(int pageIndex, int pageSize, string keyword)
        {
            var query = _settingRepository.Table;
            return new PagedList<Setting>(
                query,
                pageIndex,
                pageSize
                ).GetDto();
        }

        public PagedList<Setting> GetSettings(
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            string keyword = null)
        {
            var query = _settingRepository.Table;

            return new PagedList<Setting>(
                query,
                pageIndex,
                pageSize
                );
        }

        public string GetValue(string key)
        {
            return Get(key)?.Value;
        }

        public int InsertSetting(Setting model)
        {
            return _settingRepository.Insert(model);
        }

        public Setting Set(string key, object value, object extends)
        {
            var entity = Get(key);
            if (entity != null)
            {
                // update
                entity.Value = value.ToString();
                _settingRepository.Update(entity);
            }
            else
            {
                // insert
                entity = new Setting
                {
                    Key = key,
                    Extends = extends?.ToString(),
                    CreateOn = DateTime.Now,
                    Value = value.ToString()
                };
                _settingRepository.Insert(entity);
            }
            return entity;
        }

        public int UpdateSetting(string id, string key, string value)
        {
            var model = _settingRepository.GetById(id);
            model.Key = key;
            model.Value = value;

           return _settingRepository.Update(model);
        }

    }
}
