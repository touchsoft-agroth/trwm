using System;
using System.Collections.Generic;
using UnityEngine;

namespace trwm.Source.Actions
{
    public class ActionMap
    {
        public IEnumerable<KeyCode> KeyCodes => _bindings.Keys;
        
        private readonly Dictionary<KeyCode, Action> _bindings;
        
        public ActionMap(Dictionary<KeyCode, Action> bindings)
        {
            _bindings = bindings;
        }
        
        public void Execute(KeyCode keyCode)
        {
            if (_bindings.TryGetValue(keyCode, out var binding))
            {
                binding();
            }
        }
    }
}