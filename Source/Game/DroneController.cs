using System.Collections.Generic;
using UnityEngine;

namespace trwm.Source.Game
{
    public class DroneController
    {
        private static readonly Dictionary<MovementDirection, GridDirection> MovementMap =
            new Dictionary<MovementDirection, GridDirection>
            {
                { MovementDirection.Up, GridDirection.North },
                { MovementDirection.Down, GridDirection.South },
                { MovementDirection.Left, GridDirection.West },
                { MovementDirection.Right, GridDirection.East },
            };
        
        private readonly Drone _subject;
        private readonly Logging.Logger _logger;

        private readonly Farm _farm;
        
        public DroneController()
        {
            _subject = Saver.Inst.mainFarm.drone;
            _farm = Object.FindObjectOfType<Farm>(true);
            
            _logger = new Logging.Logger(this);
        }

        public void Move(MovementDirection direction)
        {
            var gridDirection = MovementMap[direction];
            _subject.Move(gridDirection);
            _logger.Info($"moving in direction {direction}");
        }

        public void Harvest()
        {
            _subject.Harvest();
            _logger.Info($"harvesting");
        }

        public void Place(string entityName)
        {
            _farm.gm.SetEntity(Position, entityName);
        }

        public Vector2Int Position => _subject.pos;
    }
}