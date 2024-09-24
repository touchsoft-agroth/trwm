using trwm.Source.Actions;
using trwm.Source.Game;
using UnityEngine;

namespace trwm.Source.Modes
{
    public class DroneMode : Mode
    {
        protected override ActionMap BuildActionMap(ActionMapBuilder builder)
        {
            builder.AddAction(KeyCode.J, new MoveDroneAction(builder.GameGateway, MovementDirection.Down));
            builder.AddAction(KeyCode.K, new MoveDroneAction(builder.GameGateway, MovementDirection.Up));
            builder.AddAction(KeyCode.H, new MoveDroneAction(builder.GameGateway, MovementDirection.Left));
            builder.AddAction(KeyCode.L, new MoveDroneAction(builder.GameGateway, MovementDirection.Right));

            builder.AddAction(KeyCode.A, new HarvestAction(builder.GameGateway));
            
            builder.AddModeExit();

            return builder.Build();
        }
    }
}