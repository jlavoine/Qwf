﻿using System;
using MyLibrary;
using UnityEngine;

namespace Qwf.Client {
    public class GamePiecePM : GenericViewModel, IGamePiecePM {
        public const string VALUE_PROPERTY = "Value";
        public const string ICON_PROPERTY = "PieceType";
        public const string OUTLINE_PROPERTY = "OutlineColor";
        public const string VISIBLE_PROPERTY = "IsVisible";        

        private IGamePiece mGamePiece;
        public IGamePiece GamePiece { get { return mGamePiece; } set { mGamePiece = value; } }

        private string mPlayerViewing;

        public GamePiecePM( IGamePieceData i_piece, string i_playerViewing ) {
            mPlayerViewing = i_playerViewing;

            SetProperties( i_piece );   
        }

        public virtual void Dispose() {

        }

        public void SetProperties( IGamePieceData i_piece ) {
            SetGamePiece( i_piece );
                      
            if ( i_piece != null ) {
                SetPropertiesForValidData( i_piece );
            } else {
                SetPropertiesForMissingData();
            }
        }

        public int GetValue() {
            return GamePiece.GetValue();
        }

        private void SetGamePiece( IGamePieceData i_pieceData ) {
            if ( i_pieceData != null ) {
                GamePiece = new GamePiece( i_pieceData );
            } else {
                GamePiece = null;
            }
        }

        private void SetPropertiesForValidData( IGamePieceData i_piece ) {
            SetValueProperty( i_piece.GetValue() );
            SetIconProperty( i_piece.GetPieceType() );
            SetOutlineProperty( DoesViewingPlayerOwnGamePiece() );
            SetVisibility( true );
        }

        private void SetPropertiesForMissingData() {
            SetValueProperty( 0 );
            SetIconProperty( 1 );
            SetOutlineProperty( false );
            SetVisibility( false );
        }

        public void SetVisibility( bool i_visible) {
            ViewModel.SetProperty( VISIBLE_PROPERTY, i_visible ? 1f : 0f );
        }

        private void SetValueProperty( int i_value ) {
            ViewModel.SetProperty( VALUE_PROPERTY, i_value );
        }

        private void SetIconProperty( int i_type ) {
            ViewModel.SetProperty( ICON_PROPERTY, i_type.ToString() );
        }

        private void SetOutlineProperty( bool i_doesViewingPlayerOwnPiece) {
            if ( i_doesViewingPlayerOwnPiece ) {
                ViewModel.SetProperty( OUTLINE_PROPERTY, new Color( 0, 0, 255 ) );  // TODO replace these with constants
            } else {
                ViewModel.SetProperty( OUTLINE_PROPERTY, new Color( 255, 0, 0 ) );
            }
        }

        private bool DoesViewingPlayerOwnGamePiece() {
            return mPlayerViewing == GamePiece.GetOwnerId();
        }
    }
}