using System;
using System.Collections.Generic;
using trwm.Source.Infrastructure;
using trwm.Source.Modes;
using UnityEngine;

namespace trwm.Source.Actions
{
    public class ActionMapBuilder
    {
        private readonly IStacking<Mode> _modeStack;
        private readonly ModeCollection _modeCollection;
        
        private readonly List<MappedAction> _actions;
        private bool _exitOnAction;

        private readonly Logging.Logger _logger;

        public ActionMapBuilder(IStacking<Mode> modeStack, ModeCollection modeCollection)
        {
            _modeStack = modeStack;
            _modeCollection = modeCollection;

            _actions = new List<MappedAction>();

            _logger = new Logging.Logger(this);
        }

        public ActionMap Build()
        {
            Action? onExecuteAction = null;
            if (_exitOnAction)
            {
                onExecuteAction = () => _modeStack.Pop();
            }
            
            _logger.Info($"built action map with {_actions.Count} actions");
            
            return new ActionMap(_actions, onExecuteAction);
        }
        
        public void BindAction(string name, KeyCode trigger, Action action)
        {
            _actions.Add(new MappedAction(name, action, trigger));
        }

        public void BindMode(KeyCode trigger, ModeType mode)
        {
            var action = new MappedAction($"{mode.ToDisplayName()} mode", () =>
            {
                var modeInstance = _modeCollection.Get(mode);
                if (modeInstance != null)
                {
                    _logger.Info($"pushing mode {mode} to mode stack");
                    _modeStack.Push(modeInstance);
                }
            }, trigger);
            
            _actions.Add(action);
        }

        public void AddModeExit()
        {
            _actions.Add(new MappedAction("Exit", () =>
            {
                _modeStack.Pop();
                _logger.Info("exiting current mode");
            }, KeyCode.Q));
        }

        public void ExitOnAction()
        {
            _exitOnAction = true;
        }
    }
}