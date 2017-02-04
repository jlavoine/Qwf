using MyLibrary;
using System.Collections.Generic;   

namespace Qwf.Client {
    public class GameObstaclesPM : GenericViewModel {
        private List<GameObstaclePM> mObstaclePMs;
        public List<GameObstaclePM> ObstaclePMs { get { return mObstaclePMs; } private set { mObstaclePMs = value; } }

        public GameObstaclesPM( IGameObstaclesUpdate i_data ) {
            CreateObstaclePMs( i_data );
        }

        private void CreateObstaclePMs( IGameObstaclesUpdate i_data ) {
            ObstaclePMs = new List<GameObstaclePM>();
            int count = i_data.GetObstaclesCount();
            
            for ( int i = 0; i < count; ++i ) {
                CreateObstaclePM( i_data.GetUpdate( i ) );
            }
        }

        private void CreateObstaclePM( IGameObstacleUpdate i_data ) {
            ObstaclePMs.Add( new GameObstaclePM( i_data ) );
        }
    }
}
