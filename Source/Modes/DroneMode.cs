using trwm.Source.Actions;
using trwm.Source.Game;
using UnityEngine;

namespace trwm.Source.Modes
{
    public class DroneMode : Mode
    {
        public override string Name => "Drone";

        protected override ActionMap BuildActionMap(ActionMapBuilder builder, GameGateway gameGateway)
        {
            builder.BindAction("Move down", KeyCode.J, () =>
            {
                gameGateway.Drone.Move(MovementDirection.Down);
            });
            
            builder.BindAction("Move up", KeyCode.K, () =>
            {
                gameGateway.Drone.Move(MovementDirection.Up);
            });
            builder.BindAction("Move left", KeyCode.H, () =>
            {
                gameGateway.Drone.Move(MovementDirection.Left);
            });
            builder.BindAction("Move right", KeyCode.L, () =>
            {
                gameGateway.Drone.Move(MovementDirection.Right);
            });

            builder.BindAction("Harvest", KeyCode.A, () =>
            {
                gameGateway.Drone.Harvest();
            });
            
            builder.BindMode(KeyCode.E, ModeType.DroneEntityPlacement);
            
            builder.AddModeExit();

            return builder.Build();
        }
    }
}