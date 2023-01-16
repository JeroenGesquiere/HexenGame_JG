using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HexGameSystem
{
    public class MoveSetHelper
    {
        private List<Position> _positions = new List<Position>();
        private Position _currentposition;
        private Board _board;



        public MoveSetHelper(Position currentposition, Board board)
        {
            _currentposition = currentposition;
            _board = board;
        }


        public MoveSetHelper Everything()
        {
            TileView[] allTiles = GameObject.FindObjectsOfType<TileView>();
            foreach(TileView pos in allTiles)
            {
                _positions.Add(pos.GridPosition);
            }
            return this;
        }

        public MoveSetHelper NorthEast (int maxSteps = int.MaxValue)
        {
            return Collect(new Vector2Int(1,-1), maxSteps);
        }
        public MoveSetHelper East(int maxSteps = int.MaxValue)
        {
            return Collect(new Vector2Int(1, 0), maxSteps);
        }
        public MoveSetHelper SouthEast(int maxSteps = int.MaxValue)
        {
            return Collect(new Vector2Int(0, 1), maxSteps);
        }
        public MoveSetHelper SouthWest(int maxSteps = int.MaxValue)
        {
            return Collect(new Vector2Int(-1, 1), maxSteps);
        }
        public MoveSetHelper West(int maxSteps = int.MaxValue)
        {
            return Collect(new Vector2Int(-1, 0), maxSteps);
        }
        public MoveSetHelper NorthWest(int maxSteps = int.MaxValue)
        {
            return Collect(new Vector2Int(0, -1), maxSteps);
        }
        public List<Position> ValidPositions()
        {
            return _positions;
        }


        public MoveSetHelper Collect(Vector2Int direction, int maxSteps = int.MaxValue)
        {
            var currentStep = 0;

            var position = new Position(_currentposition.Q + direction.x, _currentposition.R + direction.y);

            while(currentStep < maxSteps && _board.IsValid(position))
            {
                _positions.Add(position);

                position = new Position(position.Q + direction.x, position.R + direction.y);
                currentStep++;
            }

            return this;
        }
    }
}
