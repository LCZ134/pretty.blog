using System;
using Pretty.Core;
using Pretty.Core.Domain.Danmus;
using Pretty.Core.Extends;
using Pretty.Services.Danmus;
using Pretty.Services.Danmus.Dto;
using Pretty.Services.Dto;
using Pretty.WebFramework.Models;

namespace Pretty.WebFramework.Factories
{
    public class DanmuFactory : IDanmuFactory
    {
        private IDanmuService _danmuService;
        private IWorkContext _webContext;

        public DanmuFactory(IDanmuService danmuService, IWorkContext webContext)
        {
            _danmuService = danmuService;
            _webContext = webContext;
        }

        public ActResult<string> AddDanmu(InsertDanmuModel danmu)
        {
            var user = _webContext.User;
            var entity = danmu.Copy<Danmu>();
            entity.CreateOn = DateTime.Now;
            entity.UserId = user.Id;
            if (user == null)
                return new ActResult<string>(StatusCode.Failed, "请先登录");
            if (_danmuService.AddDanmu(entity))
            {
                return new ActResult<string>(StatusCode.Success, "添加成功");
            }
            else
            {
                return new ActResult<string>(StatusCode.Failed, "操作失败");
            }
        }

        public ActResult<string> DeleteById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return new ActResult<string>(StatusCode.Failed, "评论不存在");
            var user = _webContext.User;
            if (user == null)
                return new ActResult<string>(  StatusCode.Failed,"请先登录" );
            if (_danmuService.DeleteById(id))
            {
                return new ActResult<string>(StatusCode.Success, "操作成功");
            }
            else
            {
                return new ActResult<string>(StatusCode.Failed, "操作失败");
            }
        }

        public ActResult<string> DeleteByWordKey(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
                return new ActResult<string>(StatusCode.Failed, "参数不能为空");
            var user = _webContext.User;
            if (user == null)
                return new ActResult<string>(StatusCode.Failed, "请先登录");
            if (_danmuService.DeleteByWordKey(keyword))
            {
                return new ActResult<string>(StatusCode.Success, "操作成功");
            }
            else
            {
                return new ActResult<string>(StatusCode.Failed, "操作失败");
            }
        }

        public Paged<DanmuDto> FetchAllDanmu(DanmuPagingModel command)
        {
           return _danmuService.GetAllDanmu(
               command.Content,
               command.PageIndex, 
               command.PageSize
               );
        }

    }
}
