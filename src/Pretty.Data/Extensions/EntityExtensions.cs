using System;
using Pretty.Core.Domain;

namespace Nop.Data.Extensions
{
    /// <summary>
    /// Represents extensions
    /// </summary>
    public static class EntityExtensions
    {
        /// <summary>
        /// Check whether an entity is proxy
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Result</returns>
        private static bool IsProxy(this BaseEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            //in EF 6 we could use ObjectContext.GetObjectType. Now it's not available. Here is a workaround:

            var type = entity.GetType();
            //e.g. "CustomerProxy" will be derived from "Customer". And "Customer" is derived from BaseEntity
            return type.BaseType != null && type.BaseType.BaseType != null && type.BaseType.BaseType == typeof(BaseEntity);
        }

        
    }
}