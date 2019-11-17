using Pretty.Core;
using Pretty.Core.Extends;
using Pretty.Services.Dto;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Pretty.Services.Extends
{
    public static class PageListExtends
    {
        public static Paged<T> GetDto<T>(this IPagedList<T> pageList)
        {
            return new Paged<T>
            {
                PageIndex = pageList.PageIndex,
                PageSize = pageList.PageSize,
                HasNextPage = pageList.HasNextPage,
                HasPreviousPage = pageList.HasPreviousPage,
                Data = pageList,
                TotalCount = pageList.TotalCount,
                TotalPages = pageList.TotalPages
            };
        }

        public static Paged<S> GetDto<T, S>(this IPagedList<T> pageList) where S : class, new()
        {
            return new Paged<S>
            {
                PageIndex = pageList.PageIndex,
                PageSize = pageList.PageSize,
                HasNextPage = pageList.HasNextPage,
                HasPreviousPage = pageList.HasPreviousPage,
                Data = pageList.Where(i => i != null).Select(i => i.Copy<S>()),
                TotalCount = pageList.TotalCount,
                TotalPages = pageList.TotalPages
            };
        }

        public static Paged<S> GetDto<T, S>(this IPagedList<T> pageList, Func<T, S, S> fn)
            where S : class, new()
        {
            var data = new Paged<S>
            {
                PageIndex = pageList.PageIndex,
                PageSize = pageList.PageSize,
                HasNextPage = pageList.HasNextPage,
                HasPreviousPage = pageList.HasPreviousPage,
                Data = pageList.Select(i => fn(i, i.Copy<S>())),
                TotalCount = pageList.TotalCount,
                TotalPages = pageList.TotalPages
            };
            return data;
        }
    }
}
