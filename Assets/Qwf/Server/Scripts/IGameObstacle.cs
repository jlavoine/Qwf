using System.Collections.Generic;

namespace Qwf {
    public interface IGameObstacle {
        List<IGamePieceSlot> GetSlots();

        bool CanPieceBePlacedIntoSlot( IServerGamePiece i_piece, IGamePieceSlot i_slot );
        bool IsComplete();
        bool CanScore();

        void Score( IScoreKeeper i_scoreKeeper, IGamePlayer i_currentPlayer );
    }
}