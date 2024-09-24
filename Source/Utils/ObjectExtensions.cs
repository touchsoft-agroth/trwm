using System.Data;

namespace trwm.Source.Utils
{
    public static class ObjectExtensions
    {
        public static void ThrowIfNull(this object? obj)
        {
            if (obj == null)
            {
                throw new NoNullAllowedException();
            }
        }
    }
}