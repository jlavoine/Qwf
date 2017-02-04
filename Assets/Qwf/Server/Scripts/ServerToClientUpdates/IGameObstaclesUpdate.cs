using System.Collections.Generic;


namespace Qwf {
    public interface IGameObstaclesUpdate {
        int GetObstaclesCount();

        IGameObstacleUpdate GetUpdate( int i_index );
    }
}