using System.Collections.Generic;
using TMPro;
using trwm.Source.Infrastructure;
using trwm.Source.Utils;
using UnityEngine;

namespace trwm.Source.Game
{
    public class InputFieldTracker : GameSynced
    {
        public bool IsAnyInputFieldFocused { get; private set; }
        
        private readonly Workspace _workspace;
        private readonly List<TMP_InputField> _inputFields = new List<TMP_InputField>();

        private int _lastWindowCount = -1;

        public InputFieldTracker()
        {
            _workspace = Object.FindObjectOfType<Workspace>(true);
            _workspace.ThrowIfNull();
        }

        protected override void Update()
        {
            RegisterNewInputFields();
            PerformFocusCheck();
        }

        private void RegisterNewInputFields()
        {
            if (_workspace.container.childCount != _lastWindowCount)
            {
                var codeWindows = _workspace.container.GetComponentsInChildren<CodeWindow>();
                foreach (var codeWindow in codeWindows)
                {
                    var inputField = codeWindow.GetFieldValue<TMP_InputField>("fileNameText");
                    _inputFields.Add(inputField);
                }
            }
            
            _lastWindowCount = _workspace.container.childCount;
        }

        private void PerformFocusCheck()
        {
            IsAnyInputFieldFocused = false;
            foreach (var inputField in _inputFields)
            {
                if (!inputField.isFocused) continue;
                IsAnyInputFieldFocused = true;
                break;
            }
        }
    }
}