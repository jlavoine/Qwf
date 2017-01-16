
namespace Qwf {
    public interface IGamePieceSlot {
        int GetGamePieceType();
        int GetScoreValue();

        bool IsEmpty();
        bool CanPlacePieceIntoSlot( IGamePiece i_piece );

        void PlacePieceIntoSlot( IGamePiece i_piece );
        IGamePiece GetCurrentPiece();
    }
}
