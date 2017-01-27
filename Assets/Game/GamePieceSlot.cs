
namespace Qwf {
    public class GamePieceSlot : IGamePieceSlot {
        private GamePieceSlotData mData;
        private IGamePiece mCurrentPiece;

        public GamePieceSlot( GamePieceSlotData i_data ) {
            mData = i_data;
        }

        public int GetGamePieceType() {
            return mData.PieceType;
        }

        public int GetScoreValue() {
            return mData.ScoreValue;
        }

        public bool CanPlacePieceIntoSlot( IGamePiece i_piece ) {
            if ( !i_piece.MatchesPieceType( GetGamePieceType() ) ) {
                return false;
            }

            if ( IsEmpty() ) {
                return true;
            } else {
                if ( GetCurrentPiece().DoOwnersMatch( i_piece.GetOwner() ) ) {
                    return false;
                } else {
                    return i_piece.CanOvertakePiece( GetCurrentPiece() );
                }
            }
        }

        public void PlacePieceIntoSlot( IGamePiece i_piece ) {
            mCurrentPiece = i_piece;
        }

        public IGamePiece GetCurrentPiece() {
            return mCurrentPiece;
        }

        public bool IsEmpty() {
            return GetCurrentPiece() == null;
        }

        public void Score( IScoreKeeper i_scoreKeeper ) {
            mCurrentPiece.Score( i_scoreKeeper );
        }
    }
}