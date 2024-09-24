using UnityEngine;

namespace trwm.Source.Actions
{
    public class ActionDispatcher
    {
        private readonly ActionMap _actionMap;

        public ActionDispatcher(ActionMap actionMap)
        {
            _actionMap = actionMap;
        }

        public void Update()
        {
            foreach (var keyCode in _actionMap.KeyCodes)
            {
                if (Input.GetKeyDown(keyCode))
                {
                    _actionMap.Execute(keyCode);
                    break;
                }
            }
        }
    }
}