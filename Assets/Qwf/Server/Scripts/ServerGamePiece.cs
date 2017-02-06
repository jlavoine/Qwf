
namespace Qwf {
    public class ServerGamePiece : GamePiece, IServerGamePiece {
        private IGamePlayer mOwner;

        public ServerGamePiece( IGamePlayer i_owner, IGamePieceData i_data ) : base( i_data ) {
            mOwner = i_owner;
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