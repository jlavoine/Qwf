
namespace Qwf {
    public interface IGamePiece {
        int GetPieceType();
        int GetValue();

        bool MatchesPieceType( int i_pieceType );
        bool DoOwnersMatch( string i_playerId );
        bool CanOvertakePiece( IGamePiece i_piece );
    }
}