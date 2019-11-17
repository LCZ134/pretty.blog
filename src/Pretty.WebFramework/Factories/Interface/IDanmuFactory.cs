using Pretty.Services.Danmus.Dto;
using Pretty.Services.Dto;
using Pretty.WebFramework.Models;

namespace Pretty.WebFramework.Factories
{
    public interface IDanmuFactory
    {
        Paged<DanmuDto> FetchAllDanmu(DanmuPagingModel command);

        ActResult<string> AddDanmu(InsertDanmuModel danmu);

        ActResult<string> DeleteById(string id);

        ActResult<string> DeleteByWordKey(string keyword);

    }
}
