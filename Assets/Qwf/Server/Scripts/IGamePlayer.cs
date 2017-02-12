using System.Collections.Generic;

namespace Qwf {
    public interface IGamePlayer {
        string Id { get; }

        IServerGamePiece GetHeldPieceOfIndex( int i_index );

        bool IsGamePieceHeld( IServerGamePiece i_piece );

        void DrawToFillHand();
        void RemovePieceFromHand( IServerGamePiece i_piece );

        List<IServerGamePiece> GetHeldPieces();
        List<IServerGamePiece> GetUndrawnPieces();
    }
}