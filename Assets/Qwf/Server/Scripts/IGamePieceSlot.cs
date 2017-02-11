
namespace Qwf {
    public interface IGamePieceSlot {
        int GetGamePieceType();
        int GetScoreValue();
        int GetIndex();
        int GetObstacleIndex();

        bool IsEmpty();
        bool CanPlacePieceIntoSlot( IGamePiece i_piece );

        void PlacePieceIntoSlot( IGamePiece i_piece );
        void Score( IScoreKeeper i_scoreKeeper );

        IGamePiece GetCurrentPiece();
    }
}
