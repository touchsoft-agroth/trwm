using System;
using UnityEngine;

namespace trwm.Source.Actions
{
    public readonly struct MappedAction
    {
        public string Name { get; }
        public Action Action { get; }
        public KeyCode Key { get; }

        public MappedAction(string name, Action action, KeyCode keyCode)
        {
            Name = name;
            Action = action;
            Key = keyCode;
        }
    }
}