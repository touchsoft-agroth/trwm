using System;
using System.Collections.Generic;

namespace trwm.Source.Game.Maze
{
    // todo: convert into proper file storage
    public class MazeSeedRepository
    {
        private readonly List<StoredMazeSeed> _seeds;

        public MazeSeedRepository()
        {
            _seeds = new List<StoredMazeSeed>();
            Load();
        }

        public IEnumerable<StoredMazeSeed> GetAll()
        {
            return _seeds;
        }

        public void Add(StoredMazeSeed mazeSeed)
        {
            _seeds.Add(mazeSeed);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        private void Load()
        {
            
        }
    }
}