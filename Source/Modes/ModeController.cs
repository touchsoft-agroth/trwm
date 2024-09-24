using trwm.Source.Game;
using trwm.Source.Infrastructure;

namespace trwm.Source.Modes
{
    public class ModeController
    {
        private readonly GameGateway _gameGateway;
        
        private readonly ModeCollection _modeCollection;
        private readonly DefaultStack<Mode> _modeStack;

        private readonly Logging.Logger _logger;

        public ModeController(GameGateway gameGateway)
        {
            _gameGateway = gameGateway;

            _modeStack = new DefaultStack<Mode>();
            _modeCollection = new ModeCollection();
            
            var normalMode = new NormalMode();
            Register(ModeType.Normal, normalMode);
            _modeStack.SetDefault(normalMode);
            
            RegisterModes();

            _logger = new Logging.Logger(this);
        }

        public void Update()
        {
            // todo: this should not do anything while editing a script....
            
            var activeMode = GetActiveMode();
            activeMode.Dispatcher.Update();
        }
        
        private void RegisterModes()
        {
            Register(ModeType.Drone, new DroneMode());
            Register(ModeType.Window, new WindowMode());
            Register(ModeType.DroneEntityPlacement, new DroneEntityPlacementMode());
        }

        private void Register(ModeType modeType, Mode mode)
        {
            mode.Initialize(_gameGateway, _modeCollection, _modeStack);
            _modeCollection.Add(modeType, mode);
        }

        private Mode GetActiveMode()
        {
            return _modeStack.Peek();
        }
    }
}