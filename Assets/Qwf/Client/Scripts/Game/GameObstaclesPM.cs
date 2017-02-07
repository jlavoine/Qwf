using MyLibrary;
using System.Collections.Generic;   

namespace Qwf.Client {
    public class GameObstaclesPM : GenericViewModel {
        private List<GameObstaclePM> mObstaclePMs;
        public List<GameObstaclePM> ObstaclePMs { get { return mObstaclePMs; } private set { mObstaclePMs = value; } }

        public const int DEFAULT_OBSTACLE_COUNT = 3;    // TODO constant!

        public GameObstaclesPM() {
            CreateObstaclePMs( );
            ListenForMessages( true );
        }

        public void Dispose() {
            ListenForMessages( false );
        }

        private void ListenForMessages( bool i_listen ) {
            if ( i_listen ) {
                MyMessenger.Instance.AddListener<IGameObstaclesUpdate>( ClientMessages.UPDATE_OBSTACLES, OnUpdateFromServer );
            }
            else {
                MyMessenger.Instance.RemoveListener<IGameObstaclesUpdate>( ClientMessages.UPDATE_OBSTACLES, OnUpdateFromServer );
            }
        }

        private void CreateObstaclePMs() {
            ObstaclePMs = new List<GameObstaclePM>();
            
            for ( int i = 0; i < DEFAULT_OBSTACLE_COUNT; ++i ) {
                CreateObstaclePM( null );
            }
        }

        private void CreateObstaclePM( IGameObstacleUpdate i_data ) {
            ObstaclePMs.Add( new GameObstaclePM( i_data ) );
        }

        public void OnUpdateFromServer( IGameObstaclesUpdate i_update ) {
            int obstacleCount = i_update.GetObstaclesCount();
            for ( int i = 0; i < ObstaclePMs.Count; ++i ) {
                if ( i < obstacleCount ) {
                    ObstaclePMs[i].Update( i_update.GetUpdate( i ) );
                }
                else {
                    ObstaclePMs[i].SetVisibility( false );
                }
            }
        }
    }
}
