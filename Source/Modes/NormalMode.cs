using trwm.Source.Actions;
using UnityEngine;

namespace trwm.Source.Modes
{
    public class NormalMode : Mode
    {
        protected override ActionMap BuildActionMap(ActionMapBuilder builder)
        {
            builder.BindMode(KeyCode.D, ModeType.Drone);
            builder.BindMode(KeyCode.W, ModeType.Window);
            
            return builder.Build();
        }
    }
}