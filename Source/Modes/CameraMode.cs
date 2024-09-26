using trwm.Source.Actions;
using trwm.Source.Game;
using UnityEngine;

namespace trwm.Source.Modes
{
    public class CameraMode : Mode
    {
        public override string Name => "Camera";

        protected override ActionMap BuildActionMap(ActionMapBuilder builder, GameGateway gameGateway)
        {
            builder.BindAction("Move left", KeyCode.H, () =>
            {
                gameGateway.Workspace.Move(Vector2.left);
            });
            
            builder.BindAction("Move down", KeyCode.J, () =>
            {
                gameGateway.Workspace.Move(Vector2.down); 
            });
            
            builder.BindAction("Move up", KeyCode.K, () =>
            {
                gameGateway.Workspace.Move(Vector2.up);
            });
            
            builder.BindAction("Move right", KeyCode.L, () =>
            {
                gameGateway.Workspace.Move(Vector2.right);
            });
            
            builder.BindAction("Zoom in", KeyCode.I, () =>
            {
                gameGateway.Workspace.Zoom(1);
            });
            
            builder.BindAction("Zoom out", KeyCode.O, () =>
            {
                gameGateway.Workspace.Zoom(-1);
            });
            
            builder.BindAction("Center camera", KeyCode.C, () =>
            {
                gameGateway.Workspace.SetPosition(new Vector2(0.5f, 0.5f));
            });
            
            builder.AddModeExit();
            
            return builder.Build();
        }
    }
}