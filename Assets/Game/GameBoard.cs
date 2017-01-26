using System.Collections.Generic;

namespace Qwf {
    public class GameBoard : IGameBoard {
        private GameBoardData mData;

        private List<IGameObstacle> mRemainingObstacles = new List<IGameObstacle>();
        private List<IGameObstacle> mCurrentObstacles = new List<IGameObstacle>();

        public GameBoard( GameBoardData i_data ) {
            mData = i_data;

            CreateRemainingObstacles();
            FillCurrentObstacles();
        }

        public List<IGameObstacle> GetRemainingObstacles() {
            return mRemainingObstacles;
        }

        public List<IGameObstacle> GetCurrentObstacles() {
            return mCurrentObstacles;
        }

        public bool IsObstacleCurrent( IGameObstacle i_obstacle ) {
            return mCurrentObstacles.Contains( i_obstacle );
        }

        public void UpdateBoardState() {

        }

        private void CreateRemainingObstacles() {
            foreach ( GameObstacleData obstacleData in mData.ObstacleData ) {
                mRemainingObstacles.Add( new GameObstacle( obstacleData ) );
            }

            mRemainingObstacles.Shuffle<IGameObstacle>();
        }

        private void FillCurrentObstacles() {
            List<IGameObstacle> pickedObstacles = PickNewObstaclesToBeCurrentAndRemoveFromRemainingObstacles();
            AddObstaclesToCurrent( pickedObstacles );            
        }

        private List<IGameObstacle> PickNewObstaclesToBeCurrentAndRemoveFromRemainingObstacles() {
            List<IGameObstacle> pickedObstacles = new List<IGameObstacle>();
            int neededObstacles = mData.MaxCurrentObstacles - mCurrentObstacles.Count;

            for ( int i = 0; i < neededObstacles; ++i ) {
                if ( mRemainingObstacles.Count > 0 ) {
                    IGameObstacle obstacle = mRemainingObstacles[0];
                    mRemainingObstacles.RemoveAt( 0 );
                    pickedObstacles.Add( obstacle );
                }
            }

            return pickedObstacles;
        }

        private void AddObstaclesToCurrent( List<IGameObstacle> i_obstacles ) {
            foreach ( IGameObstacle obstacle in i_obstacles ) {
                mCurrentObstacles.Add( obstacle );
            }
        }
    }
}