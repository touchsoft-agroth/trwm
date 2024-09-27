using TMPro;
using trwm.Source.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace trwm.Source.Game
{
    public class InputsBlockedChecker
    {
        private readonly ResearchMenu _researchMenu;
        private readonly Menu _menu;
        private readonly InputFieldTracker _inputFieldTracker;

        public InputsBlockedChecker()
        {
            _researchMenu = Object.FindObjectOfType<ResearchMenu>(true);
            _researchMenu.ThrowIfNull();

            _menu = Object.FindObjectOfType<Menu>(true);
            _menu.ThrowIfNull();

            _inputFieldTracker = new InputFieldTracker();
        }

        public bool IsBlocked()
        {
            var currentSelected = GetCurrentSelectedGameObject();
            if (currentSelected != null)
            {
                var inputField = currentSelected.GetComponent<CodeInputField>();
                if (inputField != null)
                {
                    if (inputField.isFocused)
                    {
                        return true;
                    }
                }
            }
            
            return _researchMenu.IsOpen || _menu.gameObject.activeInHierarchy || _inputFieldTracker.IsAnyInputFieldFocused;
        }

        private GameObject? GetCurrentSelectedGameObject()
        {
            var currentEventSystem = EventSystem.current;
            if (currentEventSystem == null)
            {
                return null;
            }

            return currentEventSystem.currentSelectedGameObject;
        }
    }
}