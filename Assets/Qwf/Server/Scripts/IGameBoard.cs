using System.Collections.Generic;

namespace Qwf {
    public interface IGameBoard {
        List<IGameObstacle> GetRemainingObstacles();
        List<IGameObstacle> GetCurrentObstacles();

        IGameObstacle GetCurrentObstacleOfIndex( int i_index );

        bool IsObstacleCurrent( IGameObstacle i_obstacle );
        bool IsGameOver();

        void UpdateBoardState( IScoreKeeper i_scoreKeeper, IGamePlayer i_currentPlayer );
    }
}