using UnityEngine;

namespace trwm.Source
{
    public class ManualDroneController
    {
        private bool _isInManualMode;
        
        public void Update()
        {
            CheckModeToggle();

            if (_isInManualMode)
            {
                UpdateManualMode();
            }
        }

        public void UpdateGui()
        {
            if (_isInManualMode)
            {
                DrawGui();
            }
        }

        private void CheckModeToggle()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _isInManualMode = !_isInManualMode;
            }
        }

        private void DrawGui()
        {
            GUI.Label(new Rect(20, 150, 200, 200), "<b><color=cyan><size=20>Manual mode enabled</size></color></b>");
        }
        
        private void UpdateManualMode()
        {
            var drone = Saver.Inst.mainFarm.drone;
            
            var inputDirection = GetInputDirection();
            if (inputDirection != null)
            {
                drone.Move(inputDirection.Value);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                drone.Harvest();
            }
        }

        private GridDirection? GetInputDirection()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                return GridDirection.South;
            }
            
            if (Input.GetKeyDown(KeyCode.K))
            {
                return GridDirection.North;
            }
            
            if (Input.GetKeyDown(KeyCode.H))
            {
                return GridDirection.West;
            }
            
            if (Input.GetKeyDown(KeyCode.L))
            {
                return GridDirection.East;
            }
            
            return null;
        }
    }
}