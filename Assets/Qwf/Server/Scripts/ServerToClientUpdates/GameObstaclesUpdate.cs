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

        public static GameObstaclesUpdate Create( List<IGameObstacle> i_obstacles ) {
            GameObstaclesUpdate update = new GameObstaclesUpdate();
            update.Obstacles = new List<GameObstacleUpdate>();

            int index = 0;
            foreach ( IGameObstacle obstacle in i_obstacles ) {
                GameObstacleUpdate obstacleUpdate = GameObstacleUpdate.Create( obstacle, index );
                update.Obstacles.Add( obstacleUpdate );

                index++;
            }

            return update;
        }
    }
}
