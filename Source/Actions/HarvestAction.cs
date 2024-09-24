using trwm.Source.Game;

namespace trwm.Source.Actions
{
    public class HarvestAction : GameAction
    {
        private readonly GameGateway _gameGateway;

        public HarvestAction(GameGateway gameGateway)
        {
            _gameGateway = gameGateway;
        }

        public override void Execute()
        {
            _gameGateway.Drone.Harvest();
        }
    }
}