using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace trwm.Source.Actions
{
    public class ActionMap
    {
        public IEnumerable<KeyCode> KeyCodes => _map.Keys.Concat(_modeBindings.Keys);
        
        private readonly Dictionary<KeyCode, GameAction> _map;
        private readonly Dictionary<KeyCode, Action> _modeBindings;
        
        private readonly Logging.Logger _logger;
        
        
        public ActionMap(Dictionary<KeyCode, GameAction> map, Dictionary<KeyCode, Action> modeBindings)
        {
            _map = map;
            _modeBindings = modeBindings;
            
            _logger = new Logging.Logger(this);
            
        }
        
        public void Execute(KeyCode keyCode)
        {
            _logger.Info($"executing action for keycode {keyCode}");

            if (_map.TryGetValue(keyCode, out var gameAction))
            {
                gameAction.Execute();
            }
            
            else if (_modeBindings.TryGetValue(keyCode, out var modeBinding))
            {
                modeBinding();
            }
        }
    }
}