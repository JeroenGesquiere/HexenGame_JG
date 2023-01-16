using BoardSystem;
using HexGameSystem;
using UnityEngine;

namespace GameSystem.Views
{    public class PieceView : MonoBehaviour, IPiece
    {
        [SerializeField]
        private Player _player;

        public Position GridPosition => PositionHelper.GridPosition(transform.position);

        public Player Player => _player;

        private void Awake()
        {
            var gridPosition = PositionHelper.GridPosition(transform.position);
            transform.position = PositionHelper.WorldPosition(gridPosition);
        }

        internal void MoveTo(Position to)
        {
            transform.position = PositionHelper.WorldPosition(to);
        }
        internal void Take()
        {

            gameObject.SetActive(false);

        }
        internal void Place(Position pos)
        {
            gameObject.SetActive(true);
            MoveTo(pos);
        }
    }
}
