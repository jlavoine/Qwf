using System.Collections.Generic;

namespace Qwf {
    public class GameBoard : IGameBoard {
        private List<IGameObstacle> mRemainingObstacles = new List<IGameObstacle>();
        private List<IGameObstacle> mCurrentObstacles = new List<IGameObstacle>();

        private int mMaxCurrentObstacles;

        public GameBoard( List<IGameObstacle> i_allObstacles, int i_maxCurrentObstacles ) {
            mMaxCurrentObstacles = i_maxCurrentObstacles;

            CreateRemainingObstacles( i_allObstacles );
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

        public void UpdateBoardState( IScoreKeeper i_scoreKeeper, IGamePlayer i_currentPlayer ) {
            ScoreCompletedObstacles( i_scoreKeeper, i_currentPlayer );
            RemoveScoredObstaclesFromCurrentList();
            FillCurrentObstacles();
        }

        private void ScoreCompletedObstacles( IScoreKeeper i_scoreKeeper, IGamePlayer i_currentPlayer ) {
            foreach ( IGameObstacle obstacle in mCurrentObstacles ) {
                if ( obstacle.CanScore() ) {
                    obstacle.Score( i_scoreKeeper, i_currentPlayer );
                }
            }
        }

        private void RemoveScoredObstaclesFromCurrentList() {
            List<IGameObstacle> newCurrentList = new List<IGameObstacle>();
            foreach ( IGameObstacle obstacle in mCurrentObstacles ) {
                if ( !obstacle.CanScore() ) {
                    newCurrentList.Add( obstacle );
                }
            }

            mCurrentObstacles = newCurrentList;
        }

        private void CreateRemainingObstacles( List<IGameObstacle> i_allObstacles ) {
            foreach ( IGameObstacle obstacle in i_allObstacles ) {
                mRemainingObstacles.Add( obstacle );
            }

            mRemainingObstacles.Shuffle<IGameObstacle>();
        }

        private void FillCurrentObstacles() {
            List<IGameObstacle> pickedObstacles = PickNewObstaclesToBeCurrentAndRemoveFromRemainingObstacles();
            AddObstaclesToCurrent( pickedObstacles );            
        }

        private List<IGameObstacle> PickNewObstaclesToBeCurrentAndRemoveFromRemainingObstacles() {
            List<IGameObstacle> pickedObstacles = new List<IGameObstacle>();
            int neededObstacles = mMaxCurrentObstacles - mCurrentObstacles.Count;

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