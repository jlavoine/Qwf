using System.Collections.Generic;

namespace Qwf {
    public class GameObstaclesUpdate : IGameObstaclesUpdate {
        public List<GameObstacleUpdate> Obstacles;

        public IGameObstacleUpdate GetUpdate( int i_index ) {
            return Obstacles[i_index];
        }
    }
}
