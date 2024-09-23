using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace trwm.Source
{
    public class ManualDroneController
    {
        public bool Active => _isInManualMode;
        
        private bool _isInManualMode;
        private bool _isPlacingEntity;
        
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
            // todo: should be disabled when inside of a code window...
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _isInManualMode = !_isInManualMode;
            }
        }

        private void DrawGui()
        {
            GUI.Label(new Rect(20, 150, 200, 200), "<b><color=cyan><size=20>Manual mode enabled</size></color></b>");

            if (_isPlacingEntity)
            {
                // todo: show menu
                var sb = new StringBuilder();
                sb.Append("<b><color=white><size=20>");
                sb.AppendLine("Entity placement:");
                foreach (var kvp in _entityKeyNameMap)
                {
                    var (keycode, name) = kvp;
                    sb.AppendLine($"({keycode}) {name}");
                }

                sb.Append("</size></color></b>");
                
                GUI.Label(new Rect(20, 375, 200, 200), sb.ToString());
            }
        }
        
        private void UpdateManualMode()
        {
            var drone = Saver.Inst.mainFarm.drone;

            if (Input.GetKeyDown(KeyCode.E))
            {
                _isPlacingEntity = !_isPlacingEntity;
            }

            if (_isPlacingEntity)
            {
                UpdatePlacingEntity(drone);
                return;
            }
            
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

        private readonly Dictionary<KeyCode, string> _entityKeyNameMap = new Dictionary<KeyCode, string>
        {
            { KeyCode.G, "grass" },
            { KeyCode.P, "pumpkin" },
            { KeyCode.S, "sunflower" },
            { KeyCode.C, "cactus" },
            { KeyCode.T, "tree" },
        };

        private void UpdatePlacingEntity(Drone drone)
        {
            if (!_isPlacingEntity) return;

            var dronePos = drone.pos;

            foreach (var kvp in _entityKeyNameMap)
            {
                if (Input.GetKeyDown(kvp.Key))
                {
                    Saver.Inst.mainFarm.gm.SetEntity(dronePos, kvp.Value);
                    _isPlacingEntity = false;
                    break;
                }
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