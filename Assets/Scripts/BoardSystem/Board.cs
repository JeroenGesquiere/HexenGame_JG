using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexGameSystem
{
    public class PieceMovedventArgs : EventArgs
    {

        public Position FromPosition { get; }
        public Position ToPosition { get; }

        public PieceView PieceView { get; }

        public PieceMovedventArgs(Position from, Position To, PieceView pieceView)
        {

            FromPosition = from;
            ToPosition = To;
            PieceView = pieceView;
        }
    }

    public class PieceTakenEventArgs : EventArgs
    {

        public Position HitPosition { get; }

        public PieceView PieceView { get; }

        public PieceTakenEventArgs(PieceView pieceView, Position hit)
        {
            PieceView = pieceView;
            HitPosition = hit;
        }

    }
    public class PiecePlacedEventArgs : EventArgs
    {
        public PieceView Pieceview { get; }
        public Position PlacePositition { get; }

        public PiecePlacedEventArgs(PieceView pieceview, Position placePositition)
        {
            Pieceview = pieceview;
            PlacePositition = placePositition;
        }
    }


    public class Board
    {
        public event EventHandler<PieceMovedventArgs> Moved;
        public event EventHandler<PieceTakenEventArgs> Taken;
        public event EventHandler<PiecePlacedEventArgs> Placed;


        private int _radius;

        public Dictionary<Position, PieceView> Pieces = new Dictionary<Position, PieceView>();


        public Board(int radius)
        {
            _radius = radius;

        }

        public bool Move(Position from, Position to)
        {
            if (!IsValid(from))
            {
                return false;
            }
            if (!IsValid(to))
            {
                return false;
            }

            //staat er iets op pos

            if (Pieces.ContainsKey(to))
            {
                return false;
            }
            if (!Pieces.TryGetValue(from, out var piece))
            {
                return false;
            }

            Pieces[to] = piece;
            Pieces.Remove(from);

            OnMoveObject(new PieceMovedventArgs(from, to, piece));

            return true;

        }
        public bool Take(Position from)
        {

            if (!IsValid(from))
            {
                return false;
            }

            if (!Pieces.TryGetValue(from, out var piece))
            {
                return false;
            }

            Pieces.Remove(from);

            OnTakeObject(new PieceTakenEventArgs(piece, from));

            return true; //temp
        }
        public bool Place(Position to, PieceView pieceView)
        {
            if (!IsValid(to))
            {
                return false;
            }
            if (Pieces.ContainsKey(to))
            {
                return false;
            }
            if(Pieces.ContainsValue(pieceView))
            {
                return false;
            }

            Pieces[to] = pieceView;

            OnPlaceObject(new PiecePlacedEventArgs(pieceView, to));
            return true;

        }

        public bool IsValid(Position pos)
            => (pos.Distance >= -1 * _radius && pos.Distance <= _radius);


        protected virtual void OnMoveObject(PieceMovedventArgs eventArgs)
        {
            var handler = Moved;
            handler?.Invoke(this, eventArgs);
        }
        protected virtual void OnTakeObject(PieceTakenEventArgs eventArgs)
        {
            var handler = Taken;
            handler?.Invoke(this, eventArgs);
        }
        protected virtual void OnPlaceObject(PiecePlacedEventArgs eventArgs)
        {
            var handler = Placed;
            handler?.Invoke(this, eventArgs);
        }

    }
}
