namespace trwm.Source.Game.Maze
{
    public readonly struct StoredMazeSeed
    {
        public string Name { get; }
        public int Seed { get; }

        public StoredMazeSeed(string name, int seed)
        {
            Name = name;
            Seed = seed;
        }
    }
}