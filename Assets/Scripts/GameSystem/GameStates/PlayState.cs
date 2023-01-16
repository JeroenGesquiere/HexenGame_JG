using BoardSystem;
using GameSystem.Views;
using HexGameSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSystem.GameStates
{
    public class PlayState : State
    {
        private Engine _engine;
        private Board _board;
        private PieceView _playerPiece;
        private BoardView _boardView;
        private DeckView _deck;

        public void InitializeScene(UnityEngine.AsyncOperation obj)
        {
            _boardView = GameObject.FindObjectOfType<BoardView>();
            _board = new Board(3);

            _boardView.PositionDrop += Dropped;

            _boardView.HoverEnter += TileEnterHover;

            _boardView.HoverExit += TileExitHover;

            _engine = new Engine(_board);

            var pieceViews = GameObject.FindObjectsOfType<PieceView>();
            foreach (PieceView p in pieceViews)
            {
                if (p.Player == Player.Player)
                {
                    _playerPiece = p;
                }
                _board.Place(p.GridPosition, p);
            }

            _board.Moved += (s, e) => e.PieceView.MoveTo(e.ToPosition);
            _board.Taken += (s, e) => e.PieceView.Take();
            _board.Placed += (s, e) => e.Pieceview.Place(e.PlacePositition);

            _deck = GameObject.FindObjectOfType<DeckView>();
        }

        public override void OnEnter()
        {
            var asyncOperation = SceneManager.LoadSceneAsync("Game", LoadSceneMode.Additive);
            asyncOperation.completed += InitializeScene;
        }

        public override void OnExit()
        {
            SceneManager.UnloadSceneAsync("Game");
        }

        public override void OnSuspend()
        {
            if (_boardView != null)
            {
                _boardView.PositionDrop -= Dropped;
                _boardView.HoverEnter -= TileEnterHover;
                _boardView.HoverExit -= TileExitHover;
            }
        }

        public override void OnResume()
        {
            if (_boardView != null)
            {
                _boardView.PositionDrop -= Dropped;
                _boardView.PositionDrop += Dropped;

                _boardView.HoverEnter -= TileEnterHover;
                _boardView.HoverEnter += TileEnterHover;

                _boardView.HoverExit -= TileExitHover;
                _boardView.HoverExit += TileExitHover;
            }
        }

        private void Dropped(object sender, PositionEventArgs eventArgs)
        {
            var positionHover = eventArgs.Position;
            var card = eventArgs.Card;

            if (_engine.DoAction(_playerPiece.GridPosition, positionHover))
            {
                _deck.DeactivateCard(card);
            }
            _boardView.ActivatedPositions = null;
        }
        private void TileEnterHover(object sender, PositionEventArgs eventArgs)
        {
            var positionHover = eventArgs.Position;
            var card = eventArgs.Card;

            var tiles = _engine.ValidPosition(card, _playerPiece.GridPosition, positionHover);

            if (tiles != null)
            {
                if (tiles.ValidPositions.Contains(positionHover))
                {
                    _engine.GetActionPositions(positionHover);
                    _boardView.ActivatedPositions = tiles.ActionPositions;
                }

                if (!tiles.ValidPositions.Contains(positionHover))
                {
                    _boardView.ActivatedPositions = tiles.ValidPositions;
                }
            }
        }
        private void TileExitHover(object sender, PositionEventArgs eventArgs)
        {
            _boardView.ActivatedPositions = null;
        }
    }
}

