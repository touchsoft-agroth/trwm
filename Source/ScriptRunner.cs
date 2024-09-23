using System;
using System.Linq;
using MelonLoader;
using UnityEngine;
using Object = UnityEngine.Object;

namespace trwm.Source
{
    public class ScriptRunner
    {
        private readonly MelonLogger.Instance _loggerInstance;
        private const float TriggerWindowTime = 0.25f;

        private float _timeSinceFirstPress;
        private bool _windowIsOpen;
        
        public ScriptRunner(MelonLogger.Instance loggerInstance)
        {
            _loggerInstance = loggerInstance;
        }

        private Workspace? Workspace
        {
            get
            {
                if (_workspace == null)
                {
                    _workspace = Object.FindObjectOfType<Workspace>();
                }

                return _workspace;
            }
        }

        private Workspace? _workspace;
        

        public void Update()
        {
            if (Workspace == null)
            {
                return;
            }
            
            _timeSinceFirstPress += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (_timeSinceFirstPress > TriggerWindowTime)
                {
                    _timeSinceFirstPress = 0f;
                }

                else if (_timeSinceFirstPress < TriggerWindowTime)
                {
                    _windowIsOpen = !_windowIsOpen;
                }
            }
        }

        public void GuiUpdate()
        {
            if (Workspace == null)
            {
                return;
            }
            
            if (_windowIsOpen)
            {
                RenderInputWindow();
            }
        }

        private string _stringToEdit = string.Empty;
        private bool _hasFocus;

        private void RenderInputWindow()
        {
            Event e = Event.current;
            
            var openWindowTitles = Workspace!.openWindows.Keys.Select(v => v);
            GUI.SetNextControlName("inputField");
            _stringToEdit = GUI.TextArea(new Rect(400, 300, 200, 50), _stringToEdit, 100);

            if (!_hasFocus)
            {
                GUI.FocusControl("inputField");
                _hasFocus = true;
            }

            if (_stringToEdit.EndsWith("\n"))
            {
                _stringToEdit = _stringToEdit.Replace("\n", string.Empty);
                var inputMatchesWindow = openWindowTitles.Contains(_stringToEdit);
                if (inputMatchesWindow)
                {
                    var interpreter = Object.FindObjectOfType<Interpreter>();
                    interpreter.StartExecution(_stringToEdit);
                    _stringToEdit = string.Empty;
                    _windowIsOpen = false;
                    _hasFocus = false;
                    _loggerInstance.Msg($"Matching window found: {_stringToEdit}");
                }

                else
                {
                    _loggerInstance.Msg($"No matching found for: {_stringToEdit}");
                    _stringToEdit = string.Empty;
                }
            }
            
            if (GUI.GetNameOfFocusedControl() == "inputField" &&
                e.type == EventType.KeyDown &&
                (e.keyCode == KeyCode.Return || e.keyCode == KeyCode.KeypadEnter))
            {
                _loggerInstance.Msg("enter pressed");
                
                var inputMatchesWindow = openWindowTitles.Contains(_stringToEdit);
                if (inputMatchesWindow)
                {
                    var interpreter = Object.FindObjectOfType<Interpreter>();
                    interpreter.StartExecution(_stringToEdit);
                    _stringToEdit = string.Empty;
                    _windowIsOpen = false;
                    _hasFocus = false;
                    _loggerInstance.Msg($"Matching window found: {_stringToEdit}");
                }
                e.Use(); // Marks the event as used to prevent other GUI elements from handling it
            } 
        }
    }
}