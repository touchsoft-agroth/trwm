using trwm.Source.Game;
using trwm.Source.Infrastructure;

namespace trwm.Source.Modes
{
    public class ModeController
    {
        private readonly GameGateway _gameGateway;
        private readonly InputsBlockedChecker _inputsBlockedChecker;
        
        private readonly ModeCollection _modeCollection;
        private readonly DefaultStack<Mode> _modeStack;


        public ModeController(GameGateway gameGateway)
        {
            _gameGateway = gameGateway;
            _inputsBlockedChecker = new InputsBlockedChecker();

            _modeStack = new DefaultStack<Mode>();
            _modeCollection = new ModeCollection();
            
            var normalMode = new NormalMode();
            Register(ModeType.Normal, normalMode);
            _modeStack.SetDefault(normalMode);
            
            RegisterModes();
        }

        public void Update()
        {
            if (_inputsBlockedChecker.IsBlocked())
            {
                return;
            }
            
            var activeMode = GetActiveMode();
            activeMode.Dispatcher.Update();
        }

        
        private void RegisterModes()
        {
            Register(ModeType.Drone, new DroneMode());
            Register(ModeType.Window, new WindowMode());
            Register(ModeType.DroneEntityPlacement, new DroneEntityPlacementMode());
            Register(ModeType.Camera, new CameraMode());
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