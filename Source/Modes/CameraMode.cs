using trwm.Source.Actions;
using trwm.Source.Game;
using UnityEngine;

namespace trwm.Source.Modes
{
    public class CameraMode : Mode
    {
        protected override ActionMap BuildActionMap(ActionMapBuilder builder, GameGateway gameGateway)
        {
            builder.BindAction(KeyCode.H, () =>
            {
                gameGateway.Workspace.Move(Vector2.left);
            });
            
            builder.BindAction(KeyCode.J, () =>
            {
                gameGateway.Workspace.Move(Vector2.down); 
            });
            
            builder.BindAction(KeyCode.K, () =>
            {
                gameGateway.Workspace.Move(Vector2.up);
            });
            
            builder.BindAction(KeyCode.L, () =>
            {
                gameGateway.Workspace.Move(Vector2.right);
            });
            
            builder.BindAction(KeyCode.I, () =>
            {
                gameGateway.Workspace.Zoom(1);
            });
            
            builder.BindAction(KeyCode.O, () =>
            {
                gameGateway.Workspace.Zoom(-1);
            });
            
            builder.AddModeExit();
            
            return builder.Build();
        }
    }
}