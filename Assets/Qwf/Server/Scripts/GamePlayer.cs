﻿using System.Collections.Generic;

namespace Qwf {
    public class GamePlayer : IGamePlayer {
        private List<IGamePiece> mUndrawnPieces = new List<IGamePiece>();
        private List<IGamePiece> mHeldPieces = new List<IGamePiece>();

        private IGameRules mRules;

        // C# doesn't let you call one ctor from another...!
        public GamePlayer( IGameRules i_rules, PlayerDeckData i_deckData ) {
            mRules = i_rules;

            SetAndShuffleUndrawnPieces( i_deckData.GetListOfPiecesFromDeck( this ) );
            DrawStartingHand();
        }

        public GamePlayer( IGameRules i_rules, List<IGamePiece> i_allPieces ) {
            mRules = i_rules;

            SetAndShuffleUndrawnPieces( i_allPieces );
            DrawStartingHand();
        }      

        public List<IGamePiece> GetHeldPieces() {
            return mHeldPieces;
        }

        public List<IGamePiece> GetUndrawnPieces() {
            return mUndrawnPieces;
        }

        public bool IsGamePieceHeld( IGamePiece i_piece ) {
            return mHeldPieces.Contains( i_piece );
        }

        public int GetId() {
            return 0;
        }

        public void DrawToFillHand() {
            int numPiecesToDraw = mRules.GetPlayerHandSize() - mHeldPieces.Count;
            DrawGamePieces( numPiecesToDraw );
        }

        public void RemovePieceFromHand( IGamePiece i_piece ) {
            if ( mHeldPieces.Contains( i_piece ) ) {
                mHeldPieces.Remove( i_piece );
            }
        }

        private void SetAndShuffleUndrawnPieces( List<IGamePiece> i_allPieces ) {
            foreach ( IGamePiece piece in i_allPieces ) {
                mUndrawnPieces.Add( piece );
            }

            mUndrawnPieces.Shuffle<IGamePiece>();
        }

        private void DrawStartingHand() {
            DrawToFillHand();
        }

        private void DrawGamePieces( int i_numPieces ) {
            for ( int i = 0; i < i_numPieces; ++i ) {
                if ( mUndrawnPieces.Count > 0 ) {
                    IGamePiece pieceToDraw = mUndrawnPieces[0];
                    mUndrawnPieces.RemoveAt( 0 );
                    mHeldPieces.Add( pieceToDraw );
                }
            }
        }
    }
}
