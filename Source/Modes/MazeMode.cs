using trwm.Source.Actions;
using trwm.Source.Game;
using UnityEngine;

namespace trwm.Source.Modes
{
    public class MazeMode : Mode
    {
        public override string Name => "Maze";
        protected override ActionMap BuildActionMap(ActionMapBuilder builder, GameGateway gameGateway)
        {
            builder.BindAction("Generate random", KeyCode.R, () =>
            {
                gameGateway.Mazes.StartRandom();
            });
            
            builder.AddModeExit();

            return builder.Build();
        }
    }
}