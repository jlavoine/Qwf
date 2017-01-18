using System.Collections.Generic;

namespace Qwf {
    public interface IGameManager {
        void AttemptMoves( IGamePlayer i_player, List<IGameMove> i_moves );
    }
}