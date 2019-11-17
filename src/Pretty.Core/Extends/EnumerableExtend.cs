using System;
using System.Collections.Generic;
using System.Linq;

namespace Pretty.Core.Extends
{
    public static class EnumerableExtend
    {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> list, Action<T> fn)
        {
            foreach (var item in list)
            {
                fn.Invoke(item);
            }

            return list;
        }

        public static IEnumerable<TResult> Map<TSource, TResult>(
            this IEnumerable<TSource> list,
            Func<TSource, TResult> selector)
        {
            return list.Select(selector);
        }

        public static bool Every<T>(this IEnumerable<T> list,
            Func<T, bool> selector)
        {
            if (list == null) return true;
            foreach (var item in list)
            {
                if (!selector(item))
                    return false;
            }
            return true;
        }
    }
}
