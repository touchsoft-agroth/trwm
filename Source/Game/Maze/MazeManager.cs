using trwm.Source.Utils;

namespace trwm.Source.Game.Maze
{
    public class MazeManager
    {
        private readonly HedgeMazeGenerator _mazeGenerator;

        public MazeManager()
        {
            _mazeGenerator = new HedgeMazeGenerator();
        }
        
        private int? _activeSeed;

        public void StartRandom()
        {
            _activeSeed = RandomNumberGenerator.Next(0, int.MaxValue);
            _mazeGenerator.GenerateHedgeMaze(10, _activeSeed.Value);
        }
    }
}