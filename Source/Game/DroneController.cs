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
        
        public DroneController()
        {
            _subject = Saver.Inst.mainFarm.drone;
        }

        public void Move(MovementDirection direction)
        {
            var gridDirection = MovementMap[direction];
            _subject.Move(gridDirection);
        }

        public void Harvest()
        {
            _subject.Harvest();
        }

        public Vector2Int Position => _subject.pos;
    }
}