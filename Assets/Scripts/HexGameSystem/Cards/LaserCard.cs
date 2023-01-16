using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HexGameSystem.Cards
{
    class LaserCard: MoveSet
    {
        public LaserCard(Board board, Position hoverPos, Position playerPos) : base(board, hoverPos, playerPos)
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
                .NorthEast()
                .East()
                .SouthEast()
                .SouthWest()
                .West()
                .NorthWest()
                .ValidPositions()
                );
        }

        public override void GetActionPositions(Position hoverPosition)
        {
            ActionPositions.Clear();
            MoveSetHelper moveSetAction = new MoveSetHelper(PlayerPosition,Board);

            Vector2Int direction = new Vector2Int((HoverPosition.Q - PlayerPosition.Q), (HoverPosition.R - PlayerPosition.R));
            direction = Normalize(direction);

            if (direction != new Vector2Int(0, 0)) 
            {
                ActionPositions.AddRange(moveSetAction.Collect(direction)
                    .ValidPositions()
                    );
            }
        }
    }
}
