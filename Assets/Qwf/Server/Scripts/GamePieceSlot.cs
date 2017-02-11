
namespace Qwf {
    public class GamePieceSlot : IGamePieceSlot {
        private IGamePieceSlotData mData;
        private IGamePiece mCurrentPiece;
         
        public GamePieceSlot( IGamePieceSlotData i_data ) {
            mData = i_data;
        }

        public int GetGamePieceType() {
            return mData.GetPieceType();
        }

        public int GetScoreValue() {
            return mData.GetScoreValue();
        }

        public int GetIndex() {
            return mData.GetIndex();
        }

        public int GetObstacleIndex() {
            return mData.GetObstacleIndex();
        }

        public bool CanPlacePieceIntoSlot( IGamePiece i_piece ) {
            if ( !i_piece.MatchesPieceType( GetGamePieceType() ) ) {
                return false;
            }

            if ( IsEmpty() ) {
                return true;
            } else {
                if ( GetCurrentPiece().DoOwnersMatch( i_piece.GetOwnerId() ) ) {
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