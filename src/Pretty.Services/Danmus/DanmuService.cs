using System.Collections.Generic;
using System.Linq;
using Pretty.Core.Data;
using Pretty.Core.Domain.Danmus;
using Pretty.Services.Danmus.Dto;
using Pretty.Services.Dto;
using Pretty.Services.Extends;

namespace Pretty.Services.Danmus
{
    public class DanmuService : IDanmuService
    {
        private IRepository<Danmu> _danmuRepos;

        public DanmuService(IRepository<Danmu> danmuRepos)
        {
            _danmuRepos = danmuRepos;
        }

        public bool AddDanmu(Danmu danmu)
        {
            return _danmuRepos.Insert(danmu) > 0;
        }

        public bool DeleteById(string id)
        {
            return _danmuRepos.Delete(_danmuRepos.GetById(id)) > 0;
        }

        public bool DeleteByWordKey(string keyword)
        {
            var wordkey = _danmuRepos.Table.FirstOrDefault(s => s.Content.Contains(keyword));

            if (wordkey == null)
                return false;

            return _danmuRepos.Delete(wordkey) > 0;
        }

        public Paged<DanmuDto> GetAllDanmu(string Content, int pageIndex, int pageSize)
        {
            var query = _danmuRepos.Table;
            pageSize = pageSize <= 0 ? 10 : pageSize;
            if (!string.IsNullOrEmpty(Content))
                query = query.Where(i => i.Content.Contains(Content));

           var danmu= new PagedList<Danmu>(query, pageIndex, pageSize);

            var result = danmu.GetDto<Danmu, DanmuDto>();
            return result;
        }

        public IEnumerable<Danmu> GetDanmu()
        {
            return _danmuRepos.Table.ToList();
        }
    }
}