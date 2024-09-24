using System.Collections.Generic;
using System.Linq;
using trwm.Source.Infrastructure;
using trwm.Source.Utils;
using UnityEngine;

namespace trwm.Source.Measuring
{
    public class TimedRunUnlockTracker
    {
        private readonly LeaderboardManager _leaderboardManager;
        private readonly Farm _farm;
        private readonly Logging.Logger _logger;
        
        private ChangeDetector<bool> _runActiveDetector;
        
        private UnlockTrackerEvents _eventTracker;
        private HashSet<string> _unlocks;
        private HashSet<string> _localUnlocks;
        
        public TimedRunUnlockTracker()
        {
            _logger = new Logging.Logger(this);
            
            _leaderboardManager = Object.FindObjectOfType<LeaderboardManager>();
            _runActiveDetector = new ChangeDetector<bool>(() => _leaderboardManager.IsRunning);

            _farm = Object.FindObjectOfType<Farm>();
            _unlocks = _farm.GetFieldValue<HashSet<string>>("unlocks");

            _localUnlocks = new HashSet<string>();

            _eventTracker = new UnlockTrackerEvents();
        }
        
        public void Update()
        {
            if (_runActiveDetector.HasChanged(out var isRunning))
            {
                Reset();
            }

            if (!isRunning)
            {
                return;
            }

            if (UpgradesChanged())
            {
                _logger.Info("current unlocks:");
                var newUnlocks = _unlocks.Where(u => !_localUnlocks.Contains(u));
                var runTime = _leaderboardManager.GetFieldValue<double>("startTime");
                var unlockTime = Time.timeAsDouble - runTime;
                _logger.Info("new unlocks: ");
                foreach (var newUnlock in newUnlocks)
                {
                    _logger.Info(newUnlock);
                    if (TrackUnlock(newUnlock))
                    {
                        _logger.Info("added to tracker");
                        _eventTracker.Add(newUnlock, unlockTime);
                    }
                    
                    _localUnlocks.Add(newUnlock);
                }
            }
        }

        public void OnGuiUpdate()
        {
            GUI.Label(new Rect(100, 100, 300, 800), _eventTracker.Output());
        }

        private bool UpgradesChanged()
        {
            var changed = false;
            foreach (var currentUpgrade in _unlocks)
            {
                if (!_localUnlocks.Contains(currentUpgrade))
                {
                    changed = true;
                    break;
                }
            }

            return changed;
        }

        private bool TrackUnlock(string unlockName)
        {
            // this is so fucking dumb
            
            if (unlockName == "plant")
            {
                return true;
            }

            if (unlockName.Contains("_"))
            {
                foreach (var c in unlockName)
                {
                    if (int.TryParse(c.ToString(), out var _))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void Reset()
        {
            _eventTracker.Clear();
            _unlocks = _farm.GetFieldValue<HashSet<string>>("unlocks");
            _localUnlocks.Clear();
            foreach (var unlock in _unlocks)
            {
                _localUnlocks.Add(unlock);
            }
            
            _logger.Info($"reset. unlock count: {_unlocks.Count}");
        }
    }
}