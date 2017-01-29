
namespace Qwf {
    public class GamePiece : IGamePiece {
        private IGamePlayer mOwner;
        private int mValue;
        private int mType;

        public GamePiece( IGamePlayer i_owner, int i_type, int i_value ) {
            mOwner = i_owner;
            mValue = i_value;
            mType = i_type;
        }

        public GamePiece( IGamePlayer i_owner, GamePieceData i_data ) {
            mOwner = i_owner;
            mValue = i_data.Value;
            mType = i_data.PieceType;
        }

        public int GetPieceType() {
            return mType;
        }

        public int GetValue() {
            return mValue;
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

        public bool IsCurrentlyHeld() {
            return GetOwner().IsGamePieceHeld( this );
        }

        public void PlaceFromPlayerHandIntoSlot( IGamePieceSlot i_slot ) {
            i_slot.PlacePieceIntoSlot( this );
            mOwner.RemovePieceFromHand( this );
        }

        public void Score( IScoreKeeper i_scoreKeeper ) {
            i_scoreKeeper.AddPointsToPlayer( GetOwner(), GetValue() );
        }
    }
}