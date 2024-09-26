using trwm.Source.Actions;
using trwm.Source.Game;
using trwm.Source.Infrastructure;

namespace trwm.Source.Modes
{
    public abstract class Mode
    {
        public ActionMap ActionMap => _actionMap;
        
        public abstract string Name { get; }
        public ActionDispatcher Dispatcher { get; private set; }
        
        private ActionMap? _actionMap;
        
        public void Initialize(GameGateway gameGateway, ModeCollection modeCollection, IStacking<Mode> modeStack)
        {
            _actionMap = BuildActionMap(new ActionMapBuilder(modeStack, modeCollection), gameGateway);
            Dispatcher = new ActionDispatcher(_actionMap);
        }
        
        protected abstract ActionMap BuildActionMap(ActionMapBuilder builder, GameGateway gameGateway);
    }
}