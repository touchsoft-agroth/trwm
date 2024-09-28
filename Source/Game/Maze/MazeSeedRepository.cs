using System.Collections.Generic;
using trwm.Source.Persistence;

namespace trwm.Source.Game.Maze
{
    public class MazeSeedRepository
    {
        public IEnumerable<StoredMazeSeed> GetAll()
        {
            return ModDataStorage.SaveData.StoredMazeSeeds;
        }

        public void Add(StoredMazeSeed mazeSeed)
        {
            ModDataStorage.SaveData.StoredMazeSeeds.Add(mazeSeed);
        }
    }
}