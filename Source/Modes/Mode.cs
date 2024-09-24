using trwm.Source.Actions;
using trwm.Source.Game;
using trwm.Source.Infrastructure;

namespace trwm.Source.Modes
{
    public abstract class Mode
    {
        public ActionDispatcher Dispatcher { get; private set; }
        
        private ActionMap? _actionMap;
        
        public void Initialize(GameGateway gameGateway, ModeCollection modeCollection, DefaultStack<Mode> modeStack)
        {
            _actionMap = BuildActionMap(new ActionMapBuilder(gameGateway, modeStack, modeCollection));
            Dispatcher = new ActionDispatcher(_actionMap);
        }
        
        protected abstract ActionMap BuildActionMap(ActionMapBuilder builder);
    }
}