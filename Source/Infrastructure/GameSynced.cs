namespace trwm.Source.Infrastructure
{
    public abstract class GameSynced
    {
        protected GameSynced()
        {
            GameLoopSynchronizer.OnUpdate(Update);
            GameLoopSynchronizer.OnGui(OnGui);
        }
        
        protected virtual void Update() {}
        protected virtual void OnGui() {}
    }
}