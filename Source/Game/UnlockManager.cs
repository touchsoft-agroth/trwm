using System;
using System.Collections.Generic;
using System.Linq;
using Object = UnityEngine.Object;

namespace trwm.Source.Game
{
    public class UnlockManager
    {
        private List<UnlockSO> _unlocks;
        private Farm _farm;
        
        public UnlockManager()
        {
            _farm = Object.FindObjectOfType<Farm>();
            
            Initialize();
        }

        public IEnumerable<string> GetUnlockNames()
        {
            return _unlocks.Select(unlock => unlock.unlockName);
        }

        public int GetUnlockCount(string unlockName)
        {
            return ExtractLevel(unlockName);
        }

        public string StripLevel(string unlockName)
        {
            if (unlockName.Contains("_"))
            {
                return unlockName.Split("_")[0];
            }

            return unlockName;
        }

        private int ExtractLevel(string unlockName)
        {
            if (unlockName.Contains("_"))
            {
                var split = unlockName.Split("_");
                if (split.Length > 1)
                {
                    var secondPart = split[1];
                    if (int.TryParse(secondPart, out var parsedLevel))
                    {
                        return parsedLevel;
                    }
                }
            }

            else if (GetUnlockNames().Contains(unlockName))
            {
                return 1;
            }

            return 0;
        }

        private void Initialize()
        {
            _unlocks = ResourceManager.GetAllUnlocks().ToList();
        }

        public bool IsMultiUpgrade(string unlock)
        {
            foreach (var unlockSo in _unlocks)
            {
                if (unlockSo.unlockName == unlock)
                {
                    return unlockSo.IsMultiUnlock;
                }
            }

            throw new ArgumentException($"no unlock with name {unlock}");
        }
    }
}