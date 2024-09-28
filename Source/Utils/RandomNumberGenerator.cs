using System;

namespace trwm.Source.Utils
{
    public static class RandomNumberGenerator
    {
        private static readonly Random RandomInstance = new Random(DateTime.Now.Millisecond);

        public static int Next(int minInclusive, int maxExclusive)
        {
            return RandomInstance.Next(minInclusive, maxExclusive);
        }
    }
}