using trwm.Source.Game;
using trwm.Source.Infrastructure;

namespace trwm.Source.Modes
{
    public class ModeController : IStacking<Mode>
    {
        private readonly GameGateway _gameGateway;
        private readonly InputsBlockedChecker _inputsBlockedChecker;
        private readonly ModalUI _modalUI;
        
        private readonly ModeCollection _modeCollection;
        private readonly DefaultStack<Mode> _modeStack;


        public ModeController(GameGateway gameGateway)
        {
            _gameGateway = gameGateway;
            _inputsBlockedChecker = new InputsBlockedChecker();
            _modalUI = new ModalUI();

            _modeStack = new DefaultStack<Mode>();
            _modeCollection = new ModeCollection();
            
            var normalMode = new NormalMode();
            Register(ModeType.Normal, normalMode);
            _modeStack.SetDefault(normalMode);
            _modalUI.PushMode(normalMode);
            
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
            Register(ModeType.Farm, new FarmMode());
        }

        private void Register(ModeType modeType, Mode mode)
        {
            mode.Initialize(_gameGateway, _modeCollection, this);
            _modeCollection.Add(modeType, mode);
        }

        private Mode GetActiveMode()
        {
            return _modeStack.Peek();
        }

        public void Push(Mode element)
        {
            _modalUI.PushMode(element);
            _modeStack.Push(element);
        }

        public Mode Pop()
        {
            _modalUI.PopMode();
            return _modeStack.Pop();
        }
    }
}