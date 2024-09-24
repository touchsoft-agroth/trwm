using trwm.Source.Actions;
using trwm.Source.Game;
using UnityEngine;

namespace trwm.Source.Modes
{
    public class DroneEntityPlacementMode : Mode
    {
        protected override ActionMap BuildActionMap(ActionMapBuilder builder, GameGateway gameGateway)
        {
            builder.BindAction(KeyCode.P, () =>
            {
                gameGateway.Drone.Place("pumpkin");
            });
            
            builder.ExitOnAction();
            
            return builder.Build();
        }
    }
}