using Microsoft.AspNetCore.Mvc.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Pretty.WebFramework
{
    public static class WebUtils
    {
        public static IEnumerable<Attribute> GetAttrs(object controller, ActionDescriptor descriptor, IDictionary<string, object> actPrams)
        {
            dynamic desc = (dynamic)descriptor;
            if (desc == null) return null;
            MethodInfo actMeta = controller
                .GetType()
                .GetMethod(
                    desc.ActionName,
                    actPrams.Select(i => i.Value.GetType()).ToArray()
                );
            return actMeta?.GetCustomAttributes();
        }
    }
}
