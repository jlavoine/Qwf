
namespace Qwf {
    public interface IServerGamePiece : IGamePiece {
        bool IsCurrentlyHeld();

        IGamePlayer GetOwner();

        void PlaceFromPlayerHandIntoSlot( IGamePieceSlot i_slot );
        void Score( IScoreKeeper i_scoreKeeper );
    }
}