using System;
using System.Collections.Generic;
using System.Text;

namespace Pretty.WebFramework.Models
{
    public interface IPagingable
    {
        /// <summary>
        /// Page index
        /// </summary>
        int PageIndex { get; }

        /// <summary>
        /// Page size
        /// </summary>
        int PageSize { get; }
    }
}
