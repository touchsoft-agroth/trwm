using System.Collections.Generic;
using System.Linq;
using trwm.Source.Game;
using trwm.Source.Infrastructure;
using trwm.Source.Utils;
using UnityEngine;

namespace trwm.Source.UnlockTracking
{
    public class TimedRunUnlockTracker
    {
        private readonly LeaderboardManager _leaderboardManager;
        private readonly Farm _farm;
        private readonly Logging.Logger _logger;
        private readonly UnlockManager _unlockManager;
        
        private ChangeDetector<bool> _runActiveDetector;
        
        private HashSet<string> _unlocks;
        private HashSet<string> _localUnlocks;

        private HashSet<string> _unlocksNotGivenAtStart;

        private readonly TimedRunUnlocksGridWindow _gridWindow;
        private readonly UnlockRecords _unlockRecords;
        
        public TimedRunUnlockTracker()
        {
            _logger = new Logging.Logger(this);
            
            _leaderboardManager = Object.FindObjectOfType<LeaderboardManager>();
            _runActiveDetector = new ChangeDetector<bool>(() => _leaderboardManager.IsRunning);

            _unlockManager = new UnlockManager();

            _farm = Object.FindObjectOfType<Farm>();
            _unlocks = _farm.GetFieldValue<HashSet<string>>("unlocks");

            _localUnlocks = new HashSet<string>();

            _unlocksNotGivenAtStart = new HashSet<string>();

            _unlockRecords = new UnlockRecords();
            _gridWindow = new TimedRunUnlocksGridWindow(_unlockRecords.Records, new Rect(30, 100, 300, 300));
        }
        
        public void Update()
        {
            if (_runActiveDetector.HasChanged(out var isRunning))
            {
                Reset();
            }

            if (!isRunning)
            {
                _gridWindow.IsVisible = false;
                return;
            }

            _gridWindow.IsVisible = true;

            if (UpgradesChanged())
            {
                var newUnlocks = _unlocks.Where(u => !_localUnlocks.Contains(u));
                var runTime = _leaderboardManager.GetFieldValue<double>("startTime");
                var unlockTime = Time.timeAsDouble - runTime;
                
                foreach (var newUnlock in newUnlocks)
                {
                    _localUnlocks.Add(newUnlock);
                    
                    var strippedName = _unlockManager.StripLevel(newUnlock);
                    if (_unlocksNotGivenAtStart.Contains(strippedName))
                    {
                        var isMultiUnlock = _unlockManager.IsMultiUpgrade(strippedName);

                        if (isMultiUnlock)
                        {
                            if (newUnlock.Contains("_"))
                            {
                                // stupid cactus_seed
                                if (!int.TryParse(newUnlock.Split("_")[1], out _))
                                {
                                    return;
                                }
                                
                                var unlockCount = _unlockManager.GetUnlockCount(newUnlock);
                                _unlockRecords.Add(strippedName, unlockCount, unlockTime);
                            }
                        }

                        else
                        {
                            // one-time unlock
                            _unlockRecords.Add(strippedName, 1, unlockTime);
                        }
                    }
                }
            }
        }

        public void OnGuiUpdate()
        {
            _gridWindow.UpdateGui();
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

        private void Reset()
        {
            _unlockRecords.Reset();
            _unlocks = _farm.GetFieldValue<HashSet<string>>("unlocks");
            _localUnlocks.Clear();
            foreach (var unlock in _unlocks)
            {
                _localUnlocks.Add(unlock);
            }

            var allUpgrades = _unlockManager.GetUnlockNames();
            _unlocksNotGivenAtStart = allUpgrades.Except(_unlocks).ToHashSet();
            // manual grass because fucking fuck
            _unlocksNotGivenAtStart.Add("grass");
        }
    }
}