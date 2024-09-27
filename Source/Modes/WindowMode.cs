using trwm.Source.Actions;
using trwm.Source.Game;
using trwm.Source.UI;
using UnityEngine;

namespace trwm.Source.Modes
{
    public class WindowMode : Mode
    {
        public override string Name => "Window";

        protected override ActionMap BuildActionMap(ActionMapBuilder builder, GameGateway gameGateway)
        {
            builder.BindAction("Mini/Maximize active", KeyCode.M, () =>
            {
                if (gameGateway.Windows.ActiveWindow == null) return;
                gameGateway.Windows.ToggleMinimized(gameGateway.Windows.ActiveWindow.Value);
            });
            
            builder.BindAction("Run active", KeyCode.R, () =>
            {
                if (gameGateway.Windows.ActiveWindow == null) return;
                gameGateway.Windows.RunWindow(gameGateway.Windows.ActiveWindow.Value);
            });
            
            builder.BindAction("Goto active", KeyCode.G, () =>
            {
                var activeWindow = gameGateway.Windows.ActiveWindow;
                if (activeWindow != null)
                {
                    gameGateway.Windows.MoveTo(activeWindow.Value);
                }
            });
            
            builder.BindAction("Insert", KeyCode.I, () =>
            {
                var activeWindow = gameGateway.Windows.ActiveWindow;
                if (activeWindow != null)
                {
                    gameGateway.Windows.MakeFocused(activeWindow.Value);
                }
            });
            
            builder.BindAction("Search", KeyCode.F, () =>
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
            
            builder.BindAction("Set under active", KeyCode.U, () =>
            {
                if (gameGateway.Windows.TryGetUnderCamera(out var hoveredHandle))
                {
                    gameGateway.Windows.SetActive(hoveredHandle.Value);
                }
            });
            
            builder.BindAction("New window", KeyCode.N, () =>
            {
                var newWindow = gameGateway.Windows.NewWindow();
                gameGateway.Windows.SetActive(newWindow);
            });
            
            builder.AddModeExit();

            return builder.Build();
        }
    }
}