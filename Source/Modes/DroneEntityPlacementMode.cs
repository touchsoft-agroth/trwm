using System.Collections.Generic;
using trwm.Source.Actions;
using trwm.Source.Game;
using UnityEngine;

namespace trwm.Source.Modes
{
    public class DroneEntityPlacementMode : Mode
    {
        private static readonly Dictionary<KeyCode, string> EntityKeyCodeMap = new Dictionary<KeyCode, string>
        {
            { KeyCode.P, "pumpkin" },
            { KeyCode.S, "sunflower" },
            { KeyCode.D, "dinosaur" },
            { KeyCode.C, "cactus" },
            { KeyCode.B, "bush" },
            { KeyCode.T, "tree" },
        };
        
        protected override ActionMap BuildActionMap(ActionMapBuilder builder, GameGateway gameGateway)
        {
            foreach (var kvp in EntityKeyCodeMap)
            {
                var (keyCode, entityName) = kvp;
                builder.BindAction(keyCode, () =>
                {
                    gameGateway.Drone.Place(entityName);
                });
            }
            
            builder.ExitOnAction();
            
            return builder.Build();
        }
    }
}