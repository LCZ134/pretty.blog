using Pretty.Core.Extends;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pretty.Core
{
    public class Utils
    {
        /// <summary>
        /// get random number
        /// </summary>
        /// <param name="min">mininum number</param>
        /// <param name="max">maximize number</param>
        /// <returns></returns>
        public static int GetRandomNum(int min, int max)
        {
            Random _random = new Random();
            return _random.Next(min, max);
        }

        /// <summary>
        /// new guid
        /// </summary>
        /// <param name="format">guid format</param>
        /// <returns>guid</returns>
        public static string NewGuid(string format = "N")
        {
            return Guid.NewGuid().ToString(format);
        }

        /// <summary>
        /// copy object to target object
        /// </summary>
        /// <typeparam name="T">target object</typeparam>
        /// <param name="origin">origin object</param>
        /// <returns>target object</returns>
        public static T Copy<T>(object origin)
            where T : class, new()
        {
            T target = new T();

            foreach (var tProp in target.GetProperties())
            {
                origin.CopyPropertyTo(target, tProp.Name);
            }

            return target;
        }

        /// <summary>
        /// 获取对象的属性名称集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象</param>
        /// <returns>属性集合</returns>
        public static IEnumerable<string> GetPropNames<T>(T obj)
        {
            return obj.GetProperties().Select(i => i.Name);
        }

        public static string SubStr(string str, int start, int lenght)
        {
            if (str.Length <= lenght)
                return str;
            else
                return str.Substring(start, lenght);
        }
    }
}
