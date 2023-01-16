using BoardSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameSystem.Views
{
    public class PositionEventArgs : EventArgs //dropped
    {
        public Position Position { get; }
        public CardView Card { get; }

        public PositionEventArgs(Position pos, CardView card)
        {
            Position = pos;
            Card = card;
        }
    }

    public class BoardView : MonoBehaviour
    {
        public event EventHandler<PositionEventArgs> PositionDrop;
        public event EventHandler<PositionEventArgs> HoverEnter;
        public event EventHandler<PositionEventArgs> HoverExit;

        public CardView DroppedCard;

        private Dictionary<Position, TileView> _tileViewsCached = new Dictionary<Position, TileView>();
        private List<Position> _activatedPositions = new List<Position>(0);

        public List<Position> ActivatedPositions
        {
            set
            {
                foreach (var position in _activatedPositions)
                {
                    if (_tileViewsCached.ContainsKey(position))
                    {
                        _tileViewsCached[position].Deactivate();
                    }
                }

                if (value == null)
                    _activatedPositions = new List<Position>(0);
                else
                    _activatedPositions = value;


                foreach (var position in _activatedPositions)
                    if (_tileViewsCached.ContainsKey(position))
                    {
                        _tileViewsCached[position].Activate();
                    }
            }
        }

        void Start()
        {
            foreach (var positionView in GetComponentsInChildren<TileView>())
            {
                _tileViewsCached.Add(positionView.GridPosition, positionView);
            }
        }

        internal void OnPositionViewDrop(TileView positionView)
        {
            OnPositionDrop(new PositionEventArgs(positionView.GridPosition, DroppedCard));
            Debug.Log($"{DroppedCard}");
        }

        protected virtual void OnPositionDrop(PositionEventArgs EventArgs)
        {
            var handler = PositionDrop;
            handler?.Invoke(this, EventArgs);
        }

        internal void OnTileViewEnter(TileView positionView)
        {
            OnTileEnter(new PositionEventArgs(positionView.GridPosition, DroppedCard));
        }
        protected virtual void OnTileEnter(PositionEventArgs e)
        {
            var handler = HoverEnter;
            handler?.Invoke(this, e);
        }

        internal void OnTileViewExit(TileView positionView)
        {
            OnTileExit(new PositionEventArgs(positionView.GridPosition, DroppedCard));
        }
        protected virtual void OnTileExit(PositionEventArgs e)
        {
            var handler = HoverExit;
            handler?.Invoke(this, e);
        }
    }
}
