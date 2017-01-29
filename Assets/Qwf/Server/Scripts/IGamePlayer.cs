using System.Collections.Generic;

namespace Qwf {
    public interface IGamePlayer {
        string Id { get; }

        bool IsGamePieceHeld( IGamePiece i_piece );

        void DrawToFillHand();
        void RemovePieceFromHand( IGamePiece i_piece );

        List<IGamePiece> GetHeldPieces();
        List<IGamePiece> GetUndrawnPieces();
    }
}