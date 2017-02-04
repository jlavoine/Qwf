using System.Collections.Generic;

namespace Qwf {
    public class GameObstaclesUpdate : IGameObstaclesUpdate {
        public List<GameObstacleUpdate> Obstacles;

        public int GetObstaclesCount() {
            return Obstacles.Count;
        }

        public IGameObstacleUpdate GetUpdate( int i_index ) {
            return Obstacles[i_index];
        }
    }
}
