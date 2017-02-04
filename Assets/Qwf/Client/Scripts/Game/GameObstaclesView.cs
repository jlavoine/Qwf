using MyLibrary;
using System.Collections.Generic;

namespace Qwf.Client {
    public class GameObstaclesView : GroupView {
        public List<GameObstacleView> ObstacleViews;

        public void Init( GameObstaclesPM i_pm ) {
            SetModel( i_pm.ViewModel );
            InitIndividualObstacleViews( i_pm );
        }

        private void InitIndividualObstacleViews( GameObstaclesPM i_pm ) {
            List<GameObstaclePM> allObstaclePMs = i_pm.ObstaclePMs;
            for ( int i = 0; i < ObstacleViews.Count; ++i ) {
                GameObstacleView view = ObstacleViews[i];
                view.Init( allObstaclePMs[i] );
            }
        }
    }
}
