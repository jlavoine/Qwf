
namespace Qwf {
    public class GameObstacleUpdate : IGameObstacleUpdate {
        public const string IMAGE_PREFIX = "Obstacle_";

        public string Id;
        public int FinalBlowValue;

        public string GetId() {
            return Id;
        }

        public string GetImageKey() {
            return IMAGE_PREFIX + GetId();
        }

        public int GetFinalBlowValue() {
            return FinalBlowValue;
        }
    }
}