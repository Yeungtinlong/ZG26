using System;
using System.Collections.Generic;

namespace SupportUtils
{
    public static class ReflectionHelper
    {
        public static List<Type> GetTypes(Predicate<Type> condition)
        {
            List<Type> result = new List<Type>();
            
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if(condition.Invoke(type))
                        result.Add(type);
                }
            }

            return result;
        }
    }
}