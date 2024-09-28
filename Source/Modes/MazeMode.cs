using System.Collections.Generic;
using trwm.Source.Actions;
using trwm.Source.Game;
using trwm.Source.UI;
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
            
            builder.BindAction("Save current", KeyCode.S, () =>
            {
                SearchBox.Show("Seed name", new List<string>(), result =>
                {
                    gameGateway.Mazes.SaveCurrent(result);
                });
            });
            
            builder.BindAction("Load seed", KeyCode.L, () =>
            {
                var seedNames = gameGateway.Mazes.SeedNames;
                SearchBox.Show("Load maze seed", seedNames, result =>
                {
                    gameGateway.Mazes.Load(result);
                });
            });
            
            builder.AddModeExit();

            return builder.Build();
        }
    }
}