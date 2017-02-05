
namespace Qwf {
    public class ServerGamePiece : IServerGamePiece {
        private IGamePlayer mOwner;
        private int mValue;
        private int mType;

        public ServerGamePiece( IGamePlayer i_owner, int i_type, int i_value ) {
            mOwner = i_owner;
            mValue = i_value;
            mType = i_type;
        }

        public ServerGamePiece( IGamePlayer i_owner, GamePieceData i_data ) {
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

        public bool DoOwnersMatch( string i_ownerId ) {
            return i_ownerId == GetOwner().Id;
        }

        public bool CanOvertakePiece( IServerGamePiece i_piece ) {
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