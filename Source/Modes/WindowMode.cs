using trwm.Source.Actions;
using trwm.Source.Game;
using UnityEngine;

namespace trwm.Source.Modes
{
    public class WindowMode : Mode
    {
        protected override ActionMap BuildActionMap(ActionMapBuilder builder, GameGateway gameGateway)
        {
            builder.BindAction(KeyCode.M, () =>
            {
                if (gameGateway.Windows.ActiveWindow == null) return;
                gameGateway.Windows.ToggleMinimized(gameGateway.Windows.ActiveWindow.Value);
            });
            
            builder.BindAction(KeyCode.R, () =>
            {
                if (gameGateway.Windows.ActiveWindow == null) return;
                gameGateway.Windows.RunWindow(gameGateway.Windows.ActiveWindow.Value);
            });
            
            builder.BindAction(KeyCode.J, () =>
            {
                gameGateway.Windows.SetRandomActiveLol();
            });
            
            builder.BindAction(KeyCode.G, () =>
            {
                var activeWindow = gameGateway.Windows.ActiveWindow;
                if (activeWindow != null)
                {
                    gameGateway.Windows.MoveTo(activeWindow.Value);
                }
            });
            
            builder.BindAction(KeyCode.I, () =>
            {
                var activeWindow = gameGateway.Windows.ActiveWindow;
                if (activeWindow != null)
                {
                    gameGateway.Windows.MakeFocused(activeWindow.Value);
                }
            });
            
            builder.AddModeExit();

            return builder.Build();
        }
    }
}