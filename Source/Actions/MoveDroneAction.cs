using trwm.Source.Game;

namespace trwm.Source.Actions
{
    public class MoveDroneAction : GameAction
    {
        private readonly GameGateway _gameGateway;
        private readonly MovementDirection _movementDirection;

        public MoveDroneAction(GameGateway gameGateway, MovementDirection movementDirection)
        {
            _gameGateway = gameGateway;
            _movementDirection = movementDirection;
        }

        public override void Execute()
        {
            _gameGateway.Drone.Move(_movementDirection);
        }
    }
}