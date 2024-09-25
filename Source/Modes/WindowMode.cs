using trwm.Source.Actions;
using trwm.Source.Game;
using trwm.Source.UI;
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
            
            builder.BindAction(KeyCode.F, () =>
            {
                SearchBox.Show("Window search", gameGateway.Windows.GetAllWindowTitles(), result =>
                {
                    var handle = gameGateway.Windows.FindByTitle(result);
                    if (handle.HasValue)
                    {
                        gameGateway.Windows.SetActive(handle.Value);
                        gameGateway.Windows.MoveTo(handle.Value);
                    }
                });
            });
            
            builder.BindAction(KeyCode.U, () =>
            {
                if (gameGateway.Windows.TryGetUnderCamera(out var hoveredHandle))
                {
                    gameGateway.Windows.SetActive(hoveredHandle.Value);
                }
            });
            
            builder.AddModeExit();

            return builder.Build();
        }
    }
}