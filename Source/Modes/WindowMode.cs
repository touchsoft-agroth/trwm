using trwm.Source.Actions;
using UnityEngine;

namespace trwm.Source.Modes
{
    public class WindowMode : Mode
    {
        protected override ActionMap BuildActionMap(ActionMapBuilder builder)
        {
            builder.AddAction(KeyCode.M, new TryToggleActiveWindowMinimized(builder.GameGateway));
            
            builder.AddModeExit();

            return builder.Build();
        }
    }
}