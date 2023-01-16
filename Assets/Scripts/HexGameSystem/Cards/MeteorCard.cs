
using BoardSystem;
using System.Collections.Generic;

namespace HexGameSystem.Cards
{
    public class MeteorCard : MoveSet
    {
        public MeteorCard(Board board, Position hoverPos, Position playerPos) : base(board, hoverPos, playerPos)
        {
            GetValidpositions(HoverPosition);
        }

        public override void GetActionPositions(Position hoverPosition)
        {
            ActionPositions.Clear();
            if (ValidPositions.Contains(hoverPosition))
            {
                ActionPositions.Add(hoverPosition);

                ActionPositions.AddRange(new MoveSetHelper(hoverPosition, Board)
                    .NorthEast(1)
                    .East(1)
                    .SouthEast(1)
                    .SouthWest(1)
                    .West(1)
                    .NorthWest(1)
                    .ValidPositions()
                    );
            }
        }

        public override void GetValidpositions(Position pos)
        {
            ValidPositions.AddRange(new MoveSetHelper(PlayerPosition, Board)
                .Everything()
                .ValidPositions()
                );
        }

        public override List<Position> Positions(Position playerPosition)
        {
            GetValidpositions(playerPosition);
            return ValidPositions;
        }
    }
}
