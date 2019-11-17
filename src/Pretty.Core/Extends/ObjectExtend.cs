using System;
using System.Reflection;

namespace Pretty.Core.Extends
{
    public static class ObjectExtend
    {
        /// <summary>
        /// copy object to target object
        /// </summary>
        /// <typeparam name="T">target object</typeparam>
        /// <param name="origin">origin object</param>
        /// <returns>target object</returns>
        public static T Copy<T>(this object origin)
            where T : class, new()
        {
            T target = new T();

            foreach (var tProp in target.GetProperties())
            {
                origin.CopyPropertyTo(target, tProp.Name);
            }

            return target;
        }

        public static object Copy(this object origin, Type targetType)
        {
            object obj = Activator.CreateInstance(targetType);

            foreach (var tProp in obj.GetProperties())
            {
                origin.CopyPropertyTo(obj, tProp.Name);
            }

            return obj;
        }

        /// <summary>
        /// copy object property to other object
        /// </summary>
        /// <typeparam name="T">origin object</typeparam>
        /// <param name="origin">origin object</param>
        /// <param name="target">target object</param>
        /// <param name="propName">property name</param>
        /// <returns></returns>
        public static bool CopyPropertyTo<T>(this object origin, T target, string propName)
            where T : class, new()
        {
            foreach (var oProp in origin.GetProperties())
            {
                if (oProp.Name == propName)
                {
                    origin.CopyPropertyTo(target, oProp);
                }
            }

            return false;
        }

        /// <summary>
        /// copy object property to other object
        /// </summary>
        /// <typeparam name="T">origin object</typeparam>
        /// <param name="origin">origin object</param>
        /// <param name="target">target object</param>
        /// <param name="oProp">property info</param>
        /// <returns></returns>
        public static bool CopyPropertyTo<T>(this object origin, T target, PropertyInfo oProp)
        {
            var value = oProp.GetValue(origin, null);

            foreach (var tProp in target.GetProperties())
            {
                if (tProp.Name == oProp.Name)
                {
                    if (tProp.PropertyType.Name.EndsWith("Dto"))
                    {
                        var tType = tProp.PropertyType;
                        object obj = null;
                        try
                        {
                            obj = value.Copy(tType);
                        }
                        catch (Exception)
                        {
                        }
                        tProp.SetValue(target, obj);
                    }
                    else
                    {
                        tProp.SetValue(target, value);
                    }
                    return true;
                }
            }

            return false;
        }

        public static PropertyInfo[] GetProperties(this object obj)
        {
            return obj.GetType().GetProperties();
        }
    }
}
