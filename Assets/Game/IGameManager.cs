using System.Collections.Generic;

namespace Qwf {
    public interface IGameManager {
        void AttemptMoves( List<IGameMove> i_moves );
    }
}