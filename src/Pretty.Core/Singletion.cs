using System;
using System.Collections;
using System.Reflection;

namespace Pretty.Core
{
    public class Singletion
    {
        private Singletion() { }

        private static ArrayList instances = new ArrayList();

        public static T Get<T>() where T : class
        {
            foreach (var i in instances)
            {
                if (i is T)
                    return i as T;
            }

            var instance = CreateInstance<T>();
            instances.Add(instance);
            return instance;
        }

        private static T CreateInstance<T>()
        {
            return Activator.CreateInstance<T>();
        }

        public static bool Register<T>() where T : class
        {
            var notExists = !Exists<T>();
            if (notExists)
                instances.Add(CreateInstance<T>());
            return notExists;
        }

        public static bool Exists<T>() where T : class
        {
            foreach (var instance in instances)
            {
                if (instance is T)
                    return true;
            }
            return false;
        }
    }
}
