
namespace Qwf {
    public interface IServerGamePiece {
        int GetPieceType();
        int GetValue();

        bool MatchesPieceType( int i_pieceType );
        bool DoOwnersMatch( string i_playerId );
        bool CanOvertakePiece( IServerGamePiece i_piece );
        bool IsCurrentlyHeld();

        IGamePlayer GetOwner();

        void PlaceFromPlayerHandIntoSlot( IGamePieceSlot i_slot );
        void Score( IScoreKeeper i_scoreKeeper );
    }
}