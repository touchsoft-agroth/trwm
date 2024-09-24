using trwm.Source.Game;

namespace trwm.Source.Actions
{
    public class TryToggleActiveWindowMinimized : GameAction
    {
        private readonly GameGateway _gameGateway;

        public TryToggleActiveWindowMinimized(GameGateway gameGateway)
        {
            _gameGateway = gameGateway;
        }

        public override void Execute()
        {
            if (_gameGateway.Windows.ActiveWindow != null)
            {
                _gameGateway.Windows.ToggleMinimized(_gameGateway.Windows.ActiveWindow.Value);
            }
        }
    }
}