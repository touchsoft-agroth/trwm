using System.Collections.Generic;
using System.Reflection;

namespace trwm.Source.Utils
{
    public static class ReflectionExtensions
    {
        private static readonly Dictionary<string, FieldInfo> FieldInfoCache = new Dictionary<string, FieldInfo>();
        
        public static T GetFieldValue<T>(this object obj, string fieldName)
        {
            if (!FieldInfoCache.ContainsKey(fieldName))
            {
                const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
                var field = obj.GetType().GetField(fieldName, bindingFlags);
                FieldInfoCache.Add(fieldName, field);
            }
            
            return (T)FieldInfoCache[fieldName].GetValue(obj);
        }
    }
}