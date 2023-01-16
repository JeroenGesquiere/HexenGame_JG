using BoardSystem;
using GameSystem.Views;
using HexGameSystem.Cards;
using System.Collections.Generic;

namespace HexGameSystem
{
    public class Engine
    {
        private Board _board;
        private MoveSet _moveSet;
        private List<Position> _actionPositions;

        public Engine(Board board)
        {
            _board = board;
            _actionPositions = new List<Position>();
        }

        private void GetValidPositions(CardView card, Position hovPos, Position playerPos)
        {
            if (card != null)
            {
                if (card.Type == CardType.Teleport)
                {
                    _moveSet = new TeleportCard(_board, hovPos, playerPos);
                }
                else if (card.Type == CardType.Line)
                {
                    _moveSet = new LaserCard(_board, hovPos, playerPos);
                }
                else if (card.Type == CardType.Swing)
                {
                    _moveSet = new SwingCard(_board, hovPos, playerPos);
                }
                else if (card.Type == CardType.Push)
                {
                    _moveSet = new PushCard(_board, hovPos, playerPos);
                }
                else if (card.Type == CardType.Meteor)
                {
                    _moveSet = new MeteorCard(_board, hovPos, playerPos);
                }
                // else if new card comes here
            }

        }
        public void GetActionPositions(Position hovPos)
        {
            _moveSet.GetActionPositions(hovPos);
        }
        internal MoveSet ValidPosition(CardView card, Position gridPosition, Position positionHover)
        {
            GetValidPositions(card, positionHover, gridPosition);

            return _moveSet;
        }
        public bool DoAction(Position playerPos, Position hoverPos)
        {
            if (!_moveSet.Execute(playerPos, hoverPos))
            {
                return false;
            }

            return true;
        }
    }
}
