using MyLibrary;

namespace Qwf.Client {
    public class GameObstacleView : GroupView {

        public void Init( GameObstaclePM i_pm ) {
            SetModel( i_pm.ViewModel );
        }
    }
}