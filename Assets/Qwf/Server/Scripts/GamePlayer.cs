using System.Collections.Generic;

namespace Qwf.Server {
    public class GamePlayer : IGamePlayer {
        private List<IServerGamePiece> mUndrawnPieces = new List<IServerGamePiece>();
        private List<IServerGamePiece> mHeldPieces = new List<IServerGamePiece>();

        private IGameRules mRules;

        private string mId;
        public string Id { get { return mId; } private set { mId = value; } }

        // C# doesn't let you call one ctor from another...!
        public GamePlayer( IGameRules i_rules, PlayerDeckData i_deckData, string i_id ) {
            Id = i_id;
            mRules = i_rules;

            SetAndShuffleUndrawnPieces( i_deckData.GetListOfPiecesFromDeck( this ) );
            DrawStartingHand();
        }

        public GamePlayer( IGameRules i_rules, List<IServerGamePiece> i_allPieces, string i_id ) {
            Id = i_id;
            mRules = i_rules;

            SetAndShuffleUndrawnPieces( i_allPieces );
            DrawStartingHand();
        }      

        public IServerGamePiece GetHeldPieceOfIndex( int i_index ) {
            if ( mHeldPieces.Count <= i_index || i_index < 0 ) {
                return null;
            } else {
                return mHeldPieces[i_index];
            }
        }

        public List<IServerGamePiece> GetHeldPieces() {
            return mHeldPieces;
        }

        public List<IServerGamePiece> GetUndrawnPieces() {
            return mUndrawnPieces;
        }

        public bool IsGamePieceHeld( IServerGamePiece i_piece ) {
            return mHeldPieces.Contains( i_piece );
        }

        public void DrawToFillHand() {
            int numPiecesToDraw = mRules.GetPlayerHandSize() - mHeldPieces.Count;
            DrawGamePieces( numPiecesToDraw );
        }

        public void RemovePieceFromHand( IServerGamePiece i_piece ) {
            if ( mHeldPieces.Contains( i_piece ) ) {
                mHeldPieces.Remove( i_piece );
            }
        }

        private void SetAndShuffleUndrawnPieces( List<IServerGamePiece> i_allPieces ) {
            foreach ( IServerGamePiece piece in i_allPieces ) {
                mUndrawnPieces.Add( piece );
            }

            mUndrawnPieces.Shuffle<IServerGamePiece>();
        }

        private void DrawStartingHand() {
            DrawToFillHand();
        }

        private void DrawGamePieces( int i_numPieces ) {
            for ( int i = 0; i < i_numPieces; ++i ) {
                if ( mUndrawnPieces.Count > 0 ) {
                    IServerGamePiece pieceToDraw = mUndrawnPieces[0];
                    mUndrawnPieces.RemoveAt( 0 );
                    mHeldPieces.Add( pieceToDraw );
                }
            }
        }
    }
}
