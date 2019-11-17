using Pretty.Core.Domain.Danmus;
using Pretty.Services.Danmus.Dto;
using Pretty.Services.Dto;
using System.Collections.Generic;

namespace Pretty.Services.Danmus
{
    public interface IDanmuService
    {
        IEnumerable<Danmu> GetDanmu();

        Paged<DanmuDto> GetAllDanmu(string Content, int pageIndex, int pageSize);

        bool AddDanmu(Danmu danmu);

        bool DeleteById(string id);

        bool DeleteByWordKey(string keyword);

    }
}
