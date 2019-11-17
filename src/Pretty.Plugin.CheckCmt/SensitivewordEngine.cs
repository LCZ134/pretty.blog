using System;
using System.Collections.Generic;
using System.Text;

namespace Pretty.Plugin.CheckCmt
{
    public class SensitivewordEngine
    {
        private readonly static string[] sensitiveWords = new string[] { "sb" };

        public static string Match(string str, Func<string, string> handle)
        {
            foreach (var item in sensitiveWords)
            {
                if (str.Contains(item))
                {
                    str = str.Replace(item, handle(item));
                }
            }

            return str;
        }
    }
}
