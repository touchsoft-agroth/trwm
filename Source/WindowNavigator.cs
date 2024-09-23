using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MelonLoader;
using TMPro;
using UnityEngine;

namespace trwm.Source
{
    public class WindowNavigator
    {
        private MelonLogger.Instance _loggerInstance;

        private List<CodeWindow> _allWindows;
        private CodeWindow? _activeWindow;
        private Interpreter _interpreter;
        
        public void Initialize(MelonLogger.Instance loggerInstance)
        {
            _loggerInstance = loggerInstance;
            _allWindows = Object.FindObjectsOfType<CodeWindow>().ToList();
            _loggerInstance.Msg($"loaded {_allWindows.Count} windows");
        }

        public void Update()
        {
            if (_allWindows.Count == 0)
            {
                // dumb
                _allWindows = Object.FindObjectsOfType<CodeWindow>().ToList();
                return;
            }

            // dumb
            _interpreter = Object.FindObjectOfType<Interpreter>();
            
            UpdateWindowNavigation();
            if (HasActiveWindow)
            {
                UpdateActiveWindow();
            }
        }

        private void UpdateActiveWindow()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                _loggerInstance.Msg($"starting execution");
                _interpreter.StartExecution(_activeWindow.fileName);
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                MoveActiveToViewportCenter();
            }
        }

        private enum WindowNavDirection
        {
            Up,
            Down,
            Left,
            Right,
            None
        }

        private void UpdateWindowNavigation()
        {
            var navDirection = GetInputNavDirection();
            if (navDirection == WindowNavDirection.None)
            {
                return;
            }
            
            // todo: handle none active by picking closest to the camera
            if (!HasActiveWindow)
            {
                _activeWindow = _allWindows.First();
                ColorActive(_activeWindow, null);
                return;
            }

            if (navDirection == WindowNavDirection.Left)
            {
                var activeX = _activeWindow.transform.position.x;
                var sortedByHorizontalDistance = _allWindows
                    .Where(window => window.fileName != _activeWindow.fileName)
                    .OrderBy(window => Mathf.Abs(Vector3.Distance(_activeWindow.transform.position, window.transform.position)));

                var next = sortedByHorizontalDistance.First(window => window.transform.position.x > activeX);
                if (next != null);
                {
                    var old = _activeWindow;
                    _activeWindow = next;
                    ColorActive(_activeWindow, old);
                    return;
                }
            }
            
            if (navDirection == WindowNavDirection.Right)
            {
                var activeX = _activeWindow.transform.position.x;
                var sortedByHorizontalDistance = _allWindows
                    .Where(window => window.fileName != _activeWindow.fileName)
                    .OrderBy(window => Mathf.Abs(Vector3.Distance(_activeWindow.transform.position, window.transform.position)));

                var next = sortedByHorizontalDistance.First(window => window.transform.position.x < activeX);
                if (next != null);
                {
                    var old = _activeWindow;
                    _activeWindow = next;
                    ColorActive(_activeWindow, old);
                    return;
                }
            }
            
            if (navDirection == WindowNavDirection.Up)
            {
                var activeY = _activeWindow.transform.position.y;
                var sortedByVerticalDistance = _allWindows
                    .Where(window => window.fileName != _activeWindow.fileName)
                    .OrderBy(window => Mathf.Abs(Vector3.Distance(_activeWindow.transform.position, window.transform.position)));

                var next = sortedByVerticalDistance.First(window => window.transform.position.y > activeY);
                if (next != null);
                {
                    var old = _activeWindow;
                    _activeWindow = next;
                    ColorActive(_activeWindow, old);
                    return;
                }
            }
            
            if (navDirection == WindowNavDirection.Down)
            {
                var activeY = _activeWindow.transform.position.y;
                var sortedByVerticalDistance = _allWindows
                    .Where(window => window.fileName != _activeWindow.fileName)
                    .OrderBy(window => Mathf.Abs(Vector3.Distance(_activeWindow.transform.position, window.transform.position)));

                var next = sortedByVerticalDistance.First(window => window.transform.position.y < activeY);
                if (next != null);
                {
                    var old = _activeWindow;
                    _activeWindow = next;
                    ColorActive(_activeWindow, old);
                    return;
                }
            }
            
            // if (Input.GetKeyDown(KeyCode.J))
            // {
            //     CodeWindow? old = _activeWindow;
            //     if (HasActiveWindow)
            //     {
            //         var i = _allWindows.IndexOf(_activeWindow);
            //         var next = i + 1;
            //         if (next == _allWindows.Count)
            //         {
            //             next = 0;
            //         }
            //         _activeWindow = _allWindows[next];
            //     }
            //
            //     else
            //     {
            //         _activeWindow = _allWindows.First();
            //     }
            //     
            //     ColorActive(_activeWindow, old);
            //     _loggerInstance.Msg($"set window {_activeWindow.fileName} as active");
            // }
        }

        private WindowNavDirection GetInputNavDirection()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                return WindowNavDirection.Down;
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                return WindowNavDirection.Left;
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                return WindowNavDirection.Up;
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                return WindowNavDirection.Right;
            }

            return WindowNavDirection.None;
        }

        private void ColorActive(CodeWindow newWindow, CodeWindow? old)
        {
            if (old != null)
            {
                var oldText = (TMP_InputField) (typeof(CodeWindow).GetField("fileNameText", BindingFlags.NonPublic | BindingFlags.Instance)
                    .GetValue(old));

                oldText.textComponent.color = Color.white;
            }
            
            var newText = (TMP_InputField) (typeof(CodeWindow).GetField("fileNameText", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(newWindow));

            newText.textComponent.color = Color.green;
        }
        

        private void MoveActiveToViewportCenter()
        {
            if (!HasActiveWindow)
            {
                return;
            }

            //_activeWindow!.transform.position += new Vector3(5, 0, 0);
        }

        private bool HasActiveWindow => _activeWindow != null;
    }
}