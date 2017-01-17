
namespace Qwf {
    public interface IGamePiece {
        int GetPieceType();
        int GetValue();

        bool MatchesPieceType( int i_pieceType );
        bool DoOwnersMatch( IGamePlayer i_player );
        bool CanOvertakePiece( IGamePiece i_piece );
        bool IsPieceCurrentlyHeld();

        IGamePlayer GetOwner();
    }
}