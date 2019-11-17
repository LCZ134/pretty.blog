using System;
using System.ComponentModel.DataAnnotations;

namespace Pretty.Core.Domain
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            var guid = Guid.NewGuid().ToString("N");
            Id = guid;
        }
        /// <summary>
        /// Entity Identity
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Blog Create On Date
        /// </summary>
        public DateTime? CreateOn { get; set; }
    }
}
