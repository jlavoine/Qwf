using System.Collections.Generic;

namespace Qwf {
    public interface IGameBoard {
        List<IGameObstacle> GetRemainingObstacles();
        List<IGameObstacle> GetCurrentObstacles();

        bool IsObstacleCurrent( IGameObstacle i_obstacle );

        void UpdateBoardState( IScoreKeeper i_scoreKeeper, IGamePlayer i_currentPlayer );
    }
}