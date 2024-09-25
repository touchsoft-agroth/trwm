using System.Collections.Generic;
using trwm.Source.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace trwm.Source.Game
{
    public class WindowDetector
    {
        private readonly GraphicRaycaster _graphicRaycaster;
        private readonly EventSystem _eventSystem;

        private readonly List<RaycastResult> _results = new List<RaycastResult>();
        private readonly List<Window> _windowResults = new List<Window>();

        public WindowDetector()
        {
            var btEditorCanvasGo = GameObject.Find("BTEditorCanvas");
            
            _graphicRaycaster = btEditorCanvasGo.GetComponent<GraphicRaycaster>();
            _graphicRaycaster.ThrowIfNull();

            _eventSystem = btEditorCanvasGo.AddComponent<EventSystem>();
        }

        public IEnumerable<Window> DetectWindows(Vector2 screenPosition)
        {
            _windowResults.Clear();
            
            var pointerEventData = new PointerEventData(_eventSystem)
            {
                position = screenPosition
            };

            _results.Clear();
            _graphicRaycaster.Raycast(pointerEventData, _results);

            foreach (var result in _results)
            {
                var codeWindow = result.gameObject.GetComponent<Window>();
                _windowResults.AddIfNotNull(codeWindow);
            }

            return _windowResults;
        }
    }
}