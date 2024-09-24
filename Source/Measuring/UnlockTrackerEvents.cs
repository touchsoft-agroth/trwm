using System;
using System.Collections.Generic;
using System.Text;

namespace trwm.Source.Measuring
{
    public class UnlockTrackerEvents
    {
        public int Count => _events.Count;

        // could be value tuple
        private readonly List<Tuple<string, double>> _events;

        private readonly StringBuilder _outputBuilder = new StringBuilder();

        public UnlockTrackerEvents()
        {
            _events = new List<Tuple<string, double>>();
        }

        public void Add(string eventName, double timeStamp)
        {
            var timeThisStage = CalculateTimeSinceLastEvent(timeStamp);
            _events.Add(new Tuple<string, double>(eventName, timeStamp));

            _outputBuilder.AppendLine($"{FormatTime(timeStamp)} (+{FormatTime(timeThisStage)}) - {eventName}");
        }

        public void Clear()
        {
            _events.Clear();
            _outputBuilder.Clear();
        }

        public string Output()
        {
            return _outputBuilder.ToString();
        }

        private double CalculateTimeSinceLastEvent(double currentTime)
        {
            if (_events.Count == 0)
            {
                return currentTime;
            }

            return currentTime - _events[^1].Item2;
        }

        private string FormatTime(double time)
        {
            var timeSpan = TimeSpan.FromSeconds(time);
            return $@"{((int)timeSpan.TotalHours > 0 ? (int)timeSpan.TotalHours + ":" : "")}{timeSpan:mm\:ss\.fff}"; 
        }
    }
}