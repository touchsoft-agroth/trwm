using System;
using System.Collections.Generic;
using trwm.Source.Infrastructure;
using trwm.Source.Modes;
using UnityEngine;

namespace trwm.Source.Actions
{
    public class ActionMapBuilder
    {
        private readonly DefaultStack<Mode> _modeStack;
        private readonly ModeCollection _modeCollection;
        
        private readonly Dictionary<KeyCode, Action> _actions;

        private readonly Logging.Logger _logger;

        public ActionMapBuilder(DefaultStack<Mode> modeStack, ModeCollection modeCollection)
        {
            _modeStack = modeStack;
            _modeCollection = modeCollection;

            _actions = new Dictionary<KeyCode, Action>();

            _logger = new Logging.Logger(this);
        }

        public ActionMap Build()
        {
            _logger.Info($"built action map with {_actions.Count} actions");
            return new ActionMap(_actions);
        }
        
        public void AddAction(KeyCode trigger, Action action)
        {
            _actions.Add(trigger, action);
        }

        public void BindMode(KeyCode trigger, ModeType mode)
        {
            _actions.Add(trigger, () =>
            {
                var modeInstance = _modeCollection.Get(mode);
                if (modeInstance != null)
                {
                    _logger.Info($"pushing mode {mode} to mode stack");
                    _modeStack.Push(modeInstance);
                }
            });
        }

        public void AddModeExit()
        {
            _actions.Add(KeyCode.Q, () =>
            {
                _modeStack.Pop();
                _logger.Info("exiting current mode");
            });
        }
    }
}