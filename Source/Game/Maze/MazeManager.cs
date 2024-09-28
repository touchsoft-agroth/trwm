using System.Collections.Generic;
using System.Linq;
using trwm.Source.Utils;

namespace trwm.Source.Game.Maze
{
    public class MazeManager
    {
        private readonly HedgeMazeGenerator _mazeGenerator;
        private readonly MazeSeedRepository _mazeSeedRepository;

        private readonly Logging.Logger _logger;

        public MazeManager()
        {
            _mazeGenerator = new HedgeMazeGenerator();
            _mazeSeedRepository = new MazeSeedRepository();

            _logger = new Logging.Logger(this);
        }
        
        private int? _activeSeed;

        public List<string> SeedNames => _mazeSeedRepository.GetAll().Select(storedSeed => storedSeed.Name).ToList();

        public void StartRandom()
        {
            _activeSeed = RandomNumberGenerator.Next(0, int.MaxValue);
            _mazeGenerator.GenerateHedgeMaze(10, _activeSeed.Value);
        }

        public void StartSeeded(int seed)
        {
            _activeSeed = seed;
            _mazeGenerator.GenerateHedgeMaze(10, _activeSeed.Value);
        }

        public void Load(string seedName)
        {
            foreach (var mazeSeed in _mazeSeedRepository.GetAll())
            {
                if (mazeSeed.Name == seedName)
                {
                    _logger.Info($"Loaded seed {seedName} ({mazeSeed.Seed})");
                    StartSeeded(mazeSeed.Seed);
                    break;
                }
            }
            
            _logger.Warning($"Could not load seed {seedName}. No match found");
        }

        public void SaveCurrent(string name)
        {
            if (!_activeSeed.HasValue)
            {
                return;
            }

            var mazeSeed = new StoredMazeSeed(name, _activeSeed.Value);
            _mazeSeedRepository.Add(mazeSeed);
        }
    }
}