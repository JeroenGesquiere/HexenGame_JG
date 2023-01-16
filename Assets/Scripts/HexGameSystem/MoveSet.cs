using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HexGameSystem
{
    public abstract class MoveSet
    {

        public List<Position> ValidPositions { get; set; }
        public List<Position> ActionPositions { get; set; }

        public Board Board { get; set; }

        public Position HoverPosition { get; set; }
        public Position PlayerPosition { get; set; }

        protected MoveSet(Board board, Position hoverPos, Position playerPos)
        {
            Board = board;
            HoverPosition = hoverPos;
            PlayerPosition = playerPos;
            ValidPositions = new List<Position>();
            ActionPositions = new List<Position>();
        }

        public abstract void GetActionPositions(Position hoverPosition);

        public abstract void GetValidpositions(Position pos);


        public abstract List<Position> Positions(Position playerPosition);


        internal virtual bool Execute(Position playerpos, Position hoverPos)
        {
            var validPositions = Positions(playerpos);
            if (!validPositions.Contains(hoverPos))
            {
                return false;
            }

            GetActionPositions(hoverPos);

            bool hasPieceTaken = false;
            List<PieceView> takenPieces = new List<PieceView>();
            List<Position> positionTakenPieces = new List<Position>();

            foreach (Position pos in ActionPositions)
            {
                if (Board.Pieces.TryGetValue(pos, out PieceView p))
                {
                    if(p.Player == Player.Enemy)
                    {
                        takenPieces.Add(p);
                        positionTakenPieces.Add(pos);
                        hasPieceTaken = true;
                    }                    
                }
            }

            if (hasPieceTaken)
            {
                foreach (PieceView p in takenPieces)
                {
                    int indexPos = takenPieces.IndexOf(p);

                    Board.Take(positionTakenPieces[indexPos]);
                }
            }
            return true;

        }
        public Vector2Int Normalize(Vector2Int direction)
        {
            if (direction.x > 1)
            {
                direction = new Vector2Int(1, direction.y);
            }
            if (direction.x < -1)
            {
                direction = new Vector2Int(-1, direction.y);
            }
            if (direction.y < -1)
            {
                direction = new Vector2Int(direction.x, -1);
            }
            if (direction.y > 1)
            {
                direction = new Vector2Int(direction.x, 1);
            }

            return direction;
        }
    }
}
