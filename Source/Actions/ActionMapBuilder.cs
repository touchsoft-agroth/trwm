using System;
using System.Collections.Generic;
using trwm.Source.Game;
using trwm.Source.Infrastructure;
using trwm.Source.Modes;
using UnityEngine;

namespace trwm.Source.Actions
{
    public class ActionMapBuilder
    {
        private readonly DefaultStack<Mode> _modeStack;
        private readonly ModeCollection _modeCollection;
        public GameGateway GameGateway { get; }
        
        private readonly List<Tuple<KeyCode, GameAction>> _actions;
        private readonly List<Tuple<KeyCode, ModeType>> _modeBindings;
        private bool _hasModeExit;

        private readonly Logging.Logger _logger;

        public ActionMapBuilder(GameGateway gameGateway, DefaultStack<Mode> modeStack, ModeCollection modeCollection)
        {
            _modeStack = modeStack;
            _modeCollection = modeCollection;
            GameGateway = gameGateway;
            
            _actions = new List<Tuple<KeyCode, GameAction>>();
            _modeBindings = new List<Tuple<KeyCode, ModeType>>();

            _logger = new Logging.Logger(this);
        }

        public ActionMap Build()
        {
            var actionDictionary = new Dictionary<KeyCode, GameAction>();
            foreach (var action in _actions)
            {
                actionDictionary.Add(action.Item1, action.Item2);
            }

            var modeDictionary = new Dictionary<KeyCode, Action>();
            foreach (var modeBinding in _modeBindings)
            {
                var (keyCode, modeType) = modeBinding;
                
                Action openModeAction = () =>
                {
                    var modeInstance = _modeCollection.Get(modeType);
                    if (modeInstance != null)
                    {
                        _logger.Info($"pushing mode {modeType} to mode stack");
                        _modeStack.Push(modeInstance);
                    }
                };
                
                modeDictionary.Add(keyCode, openModeAction);
            }

            if (_hasModeExit)
            {
                modeDictionary.Add(KeyCode.Q, () => _modeStack.Pop());
            }

            return new ActionMap(actionDictionary, modeDictionary);
        }
        
        public void AddAction(KeyCode trigger, GameAction action)
        {
            _actions.Add(new Tuple<KeyCode, GameAction>(trigger, action));
        }

        public void BindMode(KeyCode trigger, ModeType mode)
        {
            _modeBindings.Add(new Tuple<KeyCode, ModeType>(trigger, mode));
        }

        public void AddModeExit()
        {
            _hasModeExit = true;
        }
    }
}