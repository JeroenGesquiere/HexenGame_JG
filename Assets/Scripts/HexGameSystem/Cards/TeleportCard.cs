using BoardSystem;
using GameSystem.Views;
using System.Collections.Generic;

namespace HexGameSystem
{
    public class TeleportCard : MoveSet
    {
        public TeleportCard(Board board, Position hoverPos, Position playerPos) : base(board, hoverPos, playerPos)
        {
            GetValidpositions(HoverPosition);
        }

        public override void GetActionPositions(Position hoverPosition)
        {
            ActionPositions.Clear();
            if (!Board.Pieces.TryGetValue(hoverPosition, out PieceView p))
            {
                ActionPositions.Add(hoverPosition);
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

        internal override bool Execute(Position playerpos, Position hoverPos)
        {
            var validPositions = Positions(playerpos);
            if (!validPositions.Contains(hoverPos))
            {
                return false;
            }

            Board.Move(playerpos, hoverPos);

            return true;
        }
    }
}
