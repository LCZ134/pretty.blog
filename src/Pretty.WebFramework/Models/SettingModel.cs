using System;
using System.Collections.Generic;
using System.Text;

namespace Pretty.WebFramework.Models
{
    public class SettingModel
    {
        public SettingModel()
        {
            PageSize = 10;
        }

        public int PageIndex { get; set; }

        public int PageSize { get; set; } 

        public string Keyword { get; set; }
    }
}
