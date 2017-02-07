
namespace Qwf {
    public class GamePiece : IGamePiece {
        private IGamePieceData mData;

        public GamePiece( IGamePieceData i_data ) {
            mData = i_data;
        }

        public int GetPieceType() {
            return mData.GetPieceType();
        }

        public int GetValue() {
            return mData.GetValue();
        }

        public string GetOwnerId() {
            return mData.GetOwnerId();
        }

        public bool MatchesPieceType( int i_pieceType ) {
            return i_pieceType == 0 || GetPieceType() == 0 || GetPieceType() == i_pieceType;
        }

        public bool DoOwnersMatch( string i_ownerId ) {
            return i_ownerId == mData.GetOwnerId();
        }

        public bool CanOvertakePiece( IGamePiece i_piece ) {
            return GetValue() > i_piece.GetValue();
        }

        public void Score( IScoreKeeper i_scoreKeeper ) {
            i_scoreKeeper.AddPointsToPlayer( GetOwnerId(), GetValue() );
        }
    }
}
