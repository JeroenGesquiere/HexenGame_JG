using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HexGameSystem.Cards
{
    class PushCard : MoveSet
    {
        public PushCard(Board board, Position hoverPos, Position playerPos) : base(board, hoverPos, playerPos)
        {
            GetValidpositions(PlayerPosition);
            GetActionPositions(HoverPosition);
        }

        public override List<Position> Positions(Position playerPosition)
        {
            GetValidpositions(playerPosition);
            return ValidPositions;
        }

        public override void GetValidpositions(Position pos)
        {            
            ValidPositions.AddRange(new MoveSetHelper(PlayerPosition, Board)
                .NorthEast(1)
                .East(1)
                .SouthEast(1)
                .SouthWest(1)
                .West(1)
                .NorthWest(1)
                .ValidPositions()
                );
        }

        public override void GetActionPositions(Position hoverPosition)
        {
            ActionPositions.Clear();

            if (ValidPositions.Contains(hoverPosition))
                {
                ActionPositions.Add(hoverPosition);

                List<Position> neighbors = new List<Position>();
                MoveSetHelper hoverPosHelper = new MoveSetHelper(hoverPosition, Board);

                neighbors.AddRange(hoverPosHelper
                    .NorthEast(1)
                    .East(1)
                    .SouthEast(1)
                    .SouthWest(1)
                    .West(1)
                    .NorthWest(1)
                    .ValidPositions()
                    );

                foreach (Position pos in neighbors)
                {
                    if (ValidPositions.Contains(pos))
                    {
                        ActionPositions.Add(pos);
                    }
                }
            }

        }
        internal override bool Execute(Position playerpos, Position hoverPos)
        {
            var validPositions = Positions(playerpos);
            if (!validPositions.Contains(hoverPos))
            {
                return false;
            }

            GetActionPositions(hoverPos);

            bool hasPieceMoved = false;
            List<PieceView> MovePieces = new List<PieceView>();
            List<Position> positionMovePieces = new List<Position>();

            foreach (Position pos in ActionPositions)
            {
                if (Board.Pieces.TryGetValue(pos, out PieceView p))
                {
                    MovePieces.Add(p);
                    positionMovePieces.Add(pos);
                    hasPieceMoved = true;
                }
            }

            if (hasPieceMoved)
            {
                foreach (PieceView p in MovePieces)
                {
                    int indexPos = MovePieces.IndexOf(p);

                    Vector2Int direction = new Vector2Int((hoverPos.Q - playerpos.Q), (hoverPos.R - playerpos.R));
                    direction = Normalize(direction);

                    Position to = new Position(p.GridPosition.Q + direction.x, p.GridPosition.R + direction.y);

                    if(Board.IsValid(to))
                    {
                        Board.Move(positionMovePieces[indexPos], to);
                    }
                    else
                    {
                        Board.Take(positionMovePieces[indexPos]);
                    }                    
                }
            }

            return true;
        }
    }
}
