using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace trwm.Source.Game.Maze
{
    public class HedgeMazeGenerator
    {
        private int _mazeSize;
        private readonly GridManager _gridManager;
        private System.Random _random;

        public HedgeMazeGenerator()
        {
            _gridManager = Object.FindObjectOfType<GridManager>(true);
        }

        public void GenerateHedgeMaze(int mazeSize, int seed)
        {
            _mazeSize = mazeSize;
            
            if (_mazeSize == 1)
            {
                return;
            }

            _random = new System.Random(seed);

            bool[][] mazeGrid = new bool[_mazeSize * _mazeSize][];
            int startCell = GetRandomInt(0, _mazeSize * _mazeSize);
            mazeGrid[startCell] = new bool[] { true, true, true, true };

            List<int> cellStack = new List<int> { startCell };
            bool backtracking = false;
            List<int> deadEnds = new List<int>();

            while (cellStack.Count > 0)
            {
                int currentCell = cellStack[cellStack.Count - 1];
                int currentX = currentCell % _mazeSize;
                int currentY = currentCell / _mazeSize;

                List<GridDirection> directions = ShuffleDirections();
                bool movedToNewCell = false;

                foreach (GridDirection direction in directions)
                {
                    Vector2Int directionVector = direction.GetDirectionVector();
                    int neighborX = currentX + directionVector.x;
                    int neighborY = currentY + directionVector.y;
                    int neighborCell = neighborX + _mazeSize * neighborY;

                    if (IsValidCell(neighborX, neighborY) && mazeGrid[neighborCell] == null)
                    {
                        cellStack.Add(neighborCell);
                        mazeGrid[neighborCell] = new bool[] { true, true, true, true };
                        mazeGrid[neighborCell][(int)direction.Reverse()] = false;
                        mazeGrid[currentCell][(int)direction] = false;
                        backtracking = false;
                        movedToNewCell = true;
                        break;
                    }
                }

                if (!movedToNewCell)
                {
                    cellStack.RemoveAt(cellStack.Count - 1);
                    if (!backtracking)
                    {
                        deadEnds.Add(currentCell);
                        backtracking = true;
                    }
                }
            }

            PlaceMazeOnFarm(mazeGrid, deadEnds);
        }

        private List<GridDirection> ShuffleDirections()
        {
            return Enum.GetValues(typeof(GridDirection))
                .Cast<GridDirection>()
                .OrderBy(_ => _random.NextDouble())
                .ToList();
        }

        private bool IsValidCell(int x, int y)
        {
            return x >= 0 && y >= 0 && x < _mazeSize && y < _mazeSize;
        }

        private void PlaceMazeOnFarm(bool[][] mazeGrid, List<int> deadEnds)
        {
            _gridManager.ClearGrid(false);
            var treasureCell = deadEnds[GetRandomInt(0, deadEnds.Count)];

            for (int y = 0; y < _mazeSize; y++)
            {
                for (int x = 0; x < _mazeSize; x++)
                {
                    int cell = x + _mazeSize * y;
                    bool isTreasure = cell == treasureCell;
                    var hedgePlant = (HedgePlant)_gridManager.SetEntity(new Vector2Int(x, y), isTreasure ? "treasure" : "hedge");
                    hedgePlant.SetMesh(mazeGrid[cell]);
                }
            }
        }

        private int GetRandomInt(int minInclusive, int maxExclusive)
        {
            return _random.Next(minInclusive, maxExclusive);
        }
    }
}