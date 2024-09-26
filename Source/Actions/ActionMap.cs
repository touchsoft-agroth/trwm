using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace trwm.Source.Actions
{
    public class ActionMap
    {
        public List<MappedAction> Actions { get; }

        // todo: do not use linq
        public IEnumerable<KeyCode> KeyCodes => Actions.Select(binding => binding.Key);

        private readonly Action? _onExecuteCallback;

        public ActionMap(List<MappedAction> actions, Action? onExecuteCallback)
        {
            Actions = actions;
            _onExecuteCallback = onExecuteCallback;
        }
        
        public void Execute(KeyCode keyCode)
        {
            foreach (var binding in Actions)
            {
                if (binding.Key == keyCode)
                {
                    binding.Action();
                    _onExecuteCallback?.Invoke();
                    break;
                }
            }
        }
    }
}