using System.Collections.Generic;

namespace Qwf {
    public interface IGamePlayer {
        int GetId();

        bool IsGamePieceHeld( IGamePiece i_piece );

        List<IGamePiece> GetHeldPieces();
        List<IGamePiece> GetUndrawnPieces();
    }
}