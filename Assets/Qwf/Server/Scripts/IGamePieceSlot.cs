
namespace Qwf {
    public interface IGamePieceSlot {
        int GetGamePieceType();
        int GetScoreValue();

        bool IsEmpty();
        bool CanPlacePieceIntoSlot( IServerGamePiece i_piece );

        void PlacePieceIntoSlot( IServerGamePiece i_piece );
        void Score( IScoreKeeper i_scoreKeeper );

        IServerGamePiece GetCurrentPiece();
    }
}
