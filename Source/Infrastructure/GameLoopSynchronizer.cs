using System;
using System.Collections.Generic;
using UnityEngine;

namespace trwm.Source.Infrastructure
{
    public class GameLoopSynchronizer : MonoBehaviour
    {
        public static void OnUpdate(Action onUpdateAction)
        {
            Instance.Value.RegisterInternal(onUpdateAction, HookType.Update);
        }

        public static void OnGui(Action onGuiAction)
        {
            Instance.Value.RegisterInternal(onGuiAction, HookType.Gui);
        }

        private static readonly Lazy<GameLoopSynchronizer>
            Instance = new Lazy<GameLoopSynchronizer>(CreateSynchronizer);


        private static GameLoopSynchronizer CreateSynchronizer()
        {
            if (Instance.IsValueCreated)
            {
                throw new InvalidOperationException($"{nameof(GameLoopSynchronizer)} instance is already set");
            }

            var go = new GameObject
            {
                name = "trwm_game-loop-synchronizer"
            };
            var synchronizer = go.AddComponent<GameLoopSynchronizer>();

            return synchronizer;
        }

        private enum HookType
        {
            Update,
            Gui
        }

        private readonly Dictionary<HookType, List<Action>> _callbackCollectionMap =
            new Dictionary<HookType, List<Action>>();

        private void RegisterInternal(Action callback, HookType type)
        {
            if (!_callbackCollectionMap.ContainsKey(type))
            {
                _callbackCollectionMap.Add(type, new List<Action>());
            }
            
            _callbackCollectionMap[type].Add(callback);
        }

        private void Update()
        {
            SignalCallbacks(HookType.Update);
        }

        private void OnGUI()
        {
            SignalCallbacks(HookType.Gui);
        }

        private void SignalCallbacks(HookType hookType)
        {
            foreach (var callback in _callbackCollectionMap[hookType])
            {
                callback();
            }
        }
    }
}