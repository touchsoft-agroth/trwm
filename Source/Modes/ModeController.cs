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

            _logger = new Logging.Logger(this);
        }

        public void Update()
        {
            var activeMode = GetActiveMode();

            activeMode.Dispatcher.Update();
        }

        public void Register(ModeType modeType, Mode mode)
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