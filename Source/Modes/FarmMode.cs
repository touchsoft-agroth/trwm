using trwm.Source.Actions;
using trwm.Source.Game;
using UnityEngine;

namespace trwm.Source.Modes
{
    public class FarmMode : Mode
    {
        public override string Name => "Farm";
        
        protected override ActionMap BuildActionMap(ActionMapBuilder builder, GameGateway gameGateway)
        {
            builder.BindAction("Clear", KeyCode.C, () =>
            {
                gameGateway.Farm.Clear();
            });
            
            builder.BindAction("Expand", KeyCode.E, () =>
            {
                gameGateway.Farm.Expand();
            });
            
            builder.BindAction("Shrink", KeyCode.S, () =>
            {
                gameGateway.Farm.Shrink();
            });
            
            builder.AddModeExit();
            
            return builder.Build();
        }
    }
}