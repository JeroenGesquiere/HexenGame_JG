using HexGameSystem;
using HexGameSystem.Cards;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    private Engine _engine;
    private Board _board;
    private PieceView _playerPiece;
    private BoardView _boardView;
    private DeckView _deck;

    void Start()
    {
        _boardView = FindObjectOfType<BoardView>();
        _board = new Board(3);

        _boardView.PositionDrop += Dropped;

        _boardView.HoverEnter += TileEnterHover;

        _boardView.HoverExit += TileExitHover;

        _engine = new Engine(_board);

        var pieceViews = FindObjectsOfType<PieceView>();
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

        _deck = FindObjectOfType<DeckView>();
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

        var tiles = _engine.ValidPosition(card, _playerPiece.GridPosition, positionHover); //geef moveset

        if (tiles != null)
        {
            if (tiles.ValidPositions.Contains(positionHover) )
            {
                _engine.GetActionPositions(positionHover);
                _boardView.ActivatedPositions = tiles.ActionPositions; //test

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
