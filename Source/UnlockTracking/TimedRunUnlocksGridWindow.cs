using System.Collections.Generic;
using UnityEngine;

namespace trwm.Source.UnlockTracking
{
    public class TimedRunUnlocksGridWindow
    {
        public bool IsVisible { private get; set; }
        
        private readonly List<UnlockRecord> _dataSource;

        private Rect _windowRect;
        private Vector2 _scrollPosition;
        private int _lastRecordCount;

        public TimedRunUnlocksGridWindow(List<UnlockRecord> dataSource, Rect windowRect)
        {
            _dataSource = dataSource;
            _windowRect = windowRect;
        }
        
        public void UpdateGui()
        {
            if (!IsVisible)
            {
                return;
            }
            
            _windowRect = GUI.Window(0, _windowRect, DrawWindow, "Unlock history");
        }

        private void DrawWindow(int windowId)
        {
            const float rowHeight = 25f;
            var columnWidth = _windowRect.width / 4;
            const float headerHeight = 45f;
            var contentHeight = _windowRect.height - headerHeight - 5f; 

            // Draw header
            GUI.Label(new Rect(5, 20, columnWidth, rowHeight), "Unlock");
            GUI.Label(new Rect(columnWidth + 5, 20, columnWidth, rowHeight), "Level");
            GUI.Label(new Rect(2 * columnWidth + 5, 20, columnWidth, rowHeight), "Total Time");
            GUI.Label(new Rect(3 * columnWidth + 5, 20, columnWidth, rowHeight), "Self Time");
            
            var totalContentHeight = _dataSource.Count * rowHeight;

            if (_dataSource.Count != _lastRecordCount && totalContentHeight > contentHeight)
            {
                _scrollPosition.y = totalContentHeight - contentHeight;
            } 

            _scrollPosition = GUI.BeginScrollView(
                new Rect(0, headerHeight, _windowRect.width, contentHeight),
                _scrollPosition,
                new Rect(0, 0, _windowRect.width - 20, totalContentHeight)
            );

            for (var i = 0; i < _dataSource.Count; i++)
            {
                var yPos = i * rowHeight;
                GUI.Label(new Rect(5, yPos, columnWidth, rowHeight), _dataSource[i].Name);
                GUI.Label(new Rect(columnWidth + 5, yPos, columnWidth, rowHeight), _dataSource[i].Level.ToString());
                GUI.Label(new Rect(2 * columnWidth + 5, yPos, columnWidth, rowHeight), _dataSource[i].TotalTime.ToString("F2"));
                GUI.Label(new Rect(3 * columnWidth + 5, yPos, columnWidth, rowHeight), _dataSource[i].SelfTime.ToString("F2"));
            }

            GUI.EndScrollView();

            GUI.DragWindow(); 
            
            _lastRecordCount = _dataSource.Count;
        }
    }
}