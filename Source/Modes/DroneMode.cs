using trwm.Source.Actions;
using trwm.Source.Game;
using UnityEngine;

namespace trwm.Source.Modes
{
    public class DroneMode : Mode
    {
        protected override ActionMap BuildActionMap(ActionMapBuilder builder, GameGateway gameGateway)
        {
            builder.BindAction(KeyCode.J, () =>
            {
                gameGateway.Drone.Move(MovementDirection.Down);
            });
            
            builder.BindAction(KeyCode.K, () =>
            {
                gameGateway.Drone.Move(MovementDirection.Up);
            });
            builder.BindAction(KeyCode.H, () =>
            {
                gameGateway.Drone.Move(MovementDirection.Left);
            });
            builder.BindAction(KeyCode.L, () =>
            {
                gameGateway.Drone.Move(MovementDirection.Right);
            });

            builder.BindAction(KeyCode.A, () =>
            {
                gameGateway.Drone.Harvest();
            });
            
            builder.BindMode(KeyCode.E, ModeType.DroneEntityPlacement);
            
            builder.AddModeExit();

            return builder.Build();
        }
    }
}