
namespace Qwf {
    public class GamePiece : IGamePiece {
        private GamePieceData mData;
        private IGamePlayer mOwner;

        public GamePiece( IGamePlayer i_owner, GamePieceData i_data ) {
            mOwner = i_owner;
            mData = i_data;
        }

        public int GetPieceType() {
            return mData.PieceType;
        }

        public int GetValue() {
            return mData.Value;
        }

        public bool MatchesPieceType( int i_pieceType ) {
            return i_pieceType == 0 || GetPieceType() == 0 || GetPieceType() == i_pieceType;
        }

        public bool DoOwnersMatch( IGamePlayer i_owner ) {
            return i_owner == GetOwner();
        }

        public bool CanOvertakePiece( IGamePiece i_piece ) {
            return GetValue() > i_piece.GetValue();
        }

        public IGamePlayer GetOwner() {
            return mOwner;
        }

        public bool IsPieceCurrentlyHeld() {
            return GetOwner().IsGamePieceHeld( this );
        }
    }
}