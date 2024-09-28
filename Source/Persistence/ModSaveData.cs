using System;
using System.Collections.Generic;
using trwm.Source.Game.Maze;

namespace trwm.Source.Persistence
{
    [Serializable]
    public class ModSaveData
    {
        public List<StoredMazeSeed> StoredMazeSeeds { get; } = new List<StoredMazeSeed>();
    }
}