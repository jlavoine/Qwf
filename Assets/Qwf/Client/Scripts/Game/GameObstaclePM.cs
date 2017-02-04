using MyLibrary;

namespace Qwf.Client {
    public class GameObstaclePM : GenericViewModel, IGameObstaclePM {
        public const string IMAGE_PROPERTY = "ObstacleImage";

        public GameObstaclePM( IGameObstacleUpdate i_data ) {
            SetImageProperty( i_data );
        }

        private void SetImageProperty( IGameObstacleUpdate i_data ) {
            ViewModel.SetProperty( IMAGE_PROPERTY, i_data.GetImageKey() );
        }
    }
}
