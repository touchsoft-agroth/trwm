using trwm.Source.Actions;
using trwm.Source.Game;
using UnityEngine;

namespace trwm.Source.Modes
{
    public class WindowMode : Mode
    {
        protected override ActionMap BuildActionMap(ActionMapBuilder builder, GameGateway gameGateway)
        {
            builder.AddAction(KeyCode.M, () =>
            {
                if (gameGateway.Windows.ActiveWindow == null) return;
                gameGateway.Windows.ToggleMinimized(gameGateway.Windows.ActiveWindow.Value);
            });
            
            builder.AddAction(KeyCode.R, () =>
            {
                if (gameGateway.Windows.ActiveWindow == null) return;
                gameGateway.Windows.RunWindow(gameGateway.Windows.ActiveWindow.Value);
            });
            
            builder.AddAction(KeyCode.J, () =>
            {
                gameGateway.Windows.SetRandomActiveLol();
            });
            
            builder.AddModeExit();

            return builder.Build();
        }
    }
}