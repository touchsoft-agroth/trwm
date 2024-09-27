using trwm.Source.Utils;
using Object = UnityEngine.Object;

namespace trwm.Source.Game
{
    public class FarmController
    {
        private readonly Farm _farm;
        private readonly GridManager _gridManager;

        private readonly Logging.Logger _logger;
        
        public FarmController()
        {
            _farm = Object.FindObjectOfType<Farm>(true);
            _farm.ThrowIfNull();

            _gridManager = _farm.gm;
            
            _logger = new Logging.Logger(this);
        }

        public void Expand()
        {
            if (_gridManager.SizeLimit < 3)
            {
                _gridManager.SizeLimit = 3;
                _logger.Info($"Expanded to {_gridManager.SizeLimit}");
                return;
            }

            var nextSize = _gridManager.SizeLimit + 1;
            if (nextSize < GetExpansionUpgrades() + 1)
            {
                _gridManager.SizeLimit++;
                _logger.Info($"Expanded to {_gridManager.SizeLimit}");
            }
        }

        public void Shrink()
        {
            _gridManager.SizeLimit--;
            _logger.Info($"Shrunk to {_gridManager.SizeLimit}");
        }

        public void Clear()
        {
            _gridManager.ClearGrid(true);
            _logger.Info("Cleared farm");
        }

        private int GetExpansionUpgrades()
        {
            return _farm.GetNumUpgrades("expand");
        }
    }
}