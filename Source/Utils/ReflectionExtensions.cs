using System.Reflection;

namespace trwm.Source.Utils
{
    public static class ReflectionExtensions
    {
        public static T GetFieldValue<T>(this object obj, string fieldName)
        {
            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            
            var field = obj.GetType().GetField(fieldName, bindingFlags);
            return (T)field?.GetValue(obj);
        }
    }
}