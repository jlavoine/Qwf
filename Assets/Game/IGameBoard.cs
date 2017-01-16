using System.Collections.Generic;

namespace Qwf {
    public interface IGameBoard {
        List<IGameObstacle> GetRemainingObstacles();
        List<IGameObstacle> GetCurrentObstacles();

        void MakeMove( List<IGameMove> i_moves );
    }
}