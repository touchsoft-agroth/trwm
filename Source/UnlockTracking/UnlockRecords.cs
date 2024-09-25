using System.Collections.Generic;

namespace trwm.Source.UnlockTracking
{
    public class UnlockRecords
    {
        public List<UnlockRecord> Records { get; } = new List<UnlockRecord>();

        public void Add(string unlockName, int level, double totalTime)
        {
            var selfTime = CalculateTimeSinceLast(totalTime);

            var record = new UnlockRecord(unlockName, level, totalTime, selfTime);
            Records.Add(record);
        }

        public void Reset()
        {
            Records.Clear();
        }

        private double CalculateTimeSinceLast(double newTime)
        {
            if (Records.Count == 0)
            {
                return newTime;
            }

            return newTime - Records[^1].TotalTime;
        }
    }
}