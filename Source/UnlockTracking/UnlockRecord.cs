namespace trwm.Source.UnlockTracking
{
    public readonly struct UnlockRecord
    {
        public string Name { get; }
        public int Level { get; }
        public double TotalTime { get; }
        public double SelfTime { get; }
        
        public UnlockRecord(string name, int level, double totalTime, double selfTime)
        {
            Name = name;
            Level = level;
            TotalTime = totalTime;
            SelfTime = selfTime;
        }
    }
}