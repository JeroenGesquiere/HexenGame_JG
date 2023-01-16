using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexGameSystem.Cards
{
    class SwingCard: MoveSet
    {
        public SwingCard(Board board, Position hoverPos, Position playerPos) : base(board, hoverPos, playerPos)
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
                .ValidPositions());        }

        public override void GetActionPositions(Position hoverPosition)
        {
            ActionPositions.Clear();

            if (ValidPositions.Contains(hoverPosition))
            {
                ActionPositions.Add(hoverPosition);

                List<Position> neighbors = new List<Position>();

                neighbors.AddRange(new MoveSetHelper(hoverPosition, Board)
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
    }
}
