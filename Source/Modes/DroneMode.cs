using trwm.Source.Actions;
using trwm.Source.Game;
using UnityEngine;

namespace trwm.Source.Modes
{
    public class DroneMode : Mode
    {
        protected override ActionMap BuildActionMap(ActionMapBuilder builder, GameGateway gameGateway)
        {
            builder.AddAction(KeyCode.J, () =>
            {
                gameGateway.Drone.Move(MovementDirection.Down);
            });
            
            builder.AddAction(KeyCode.K, () =>
            {
                gameGateway.Drone.Move(MovementDirection.Up);
            });
            builder.AddAction(KeyCode.H, () =>
            {
                gameGateway.Drone.Move(MovementDirection.Left);
            });
            builder.AddAction(KeyCode.L, () =>
            {
                gameGateway.Drone.Move(MovementDirection.Right);
            });

            builder.AddAction(KeyCode.A, () =>
            {
                gameGateway.Drone.Harvest();
            });
            
            builder.AddModeExit();

            return builder.Build();
        }
    }
}