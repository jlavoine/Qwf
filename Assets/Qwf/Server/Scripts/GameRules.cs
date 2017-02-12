
namespace Qwf {
    public class GameRules : IGameRules {
        public GameRules() { }

        public int GetPlayerHandSize() {
            return 7;
        }

        public int GetMaxCurrentObstacles() {
            return 3;
        }
    }
}
