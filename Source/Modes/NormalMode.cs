using trwm.Source.Actions;
using trwm.Source.Game;
using UnityEngine;

namespace trwm.Source.Modes
{
    public class NormalMode : Mode
    {
        public override string Name => "Normal";

        protected override ActionMap BuildActionMap(ActionMapBuilder builder, GameGateway gameGateway)
        {
            builder.BindMode(KeyCode.D, ModeType.Drone);
            builder.BindMode(KeyCode.W, ModeType.Window);
            builder.BindMode(KeyCode.C, ModeType.Camera);
            builder.BindMode(KeyCode.F, ModeType.Farm);
            
            return builder.Build();
        }
    }
}