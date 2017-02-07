using MyLibrary;
using System.Collections.Generic;

namespace Qwf.Client {
    public class GameObstaclesView : GroupView {
        public List<GameObstacleView> ObstacleViews;

        void Start() {
            MyMessenger.Instance.Send<IGameObstaclesUpdate>( ClientMessages.UPDATE_OBSTACLES, null ); // send this first! otherwise the PM also gets the message
            Init();            
        }

        public void Init() {
            GameObstaclesPM pm = new GameObstaclesPM();
            SetModel( pm.ViewModel );
            InitIndividualObstacleViews( pm );
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
