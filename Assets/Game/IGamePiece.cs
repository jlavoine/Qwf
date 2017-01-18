
namespace Qwf {
    public interface IGamePiece {
        int GetPieceType();
        int GetValue();

        bool MatchesPieceType( int i_pieceType );
        bool DoOwnersMatch( IGamePlayer i_player );
        bool CanOvertakePiece( IGamePiece i_piece );
        bool IsCurrentlyHeld();

        IGamePlayer GetOwner();

        void PlaceFromPlayerHandIntoSlot( IGamePieceSlot i_slot );
    }
}