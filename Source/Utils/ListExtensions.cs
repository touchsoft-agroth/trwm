using System.Collections.Generic;

namespace trwm.Source.Utils
{
    public static class ListExtensions
    {
        public static void AddIfNotNull<T>(this List<T> list, T? element) where T : class
        {
            if (element != null)
            {
                list.Add(element);
            }
        }
    }
}