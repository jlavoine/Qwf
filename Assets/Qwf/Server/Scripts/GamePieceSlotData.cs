
namespace Qwf {
    public class GamePieceSlotData : IGamePieceSlotData {
        public int PieceType;
        public int ScoreValue;
        public int Index;
        public int ObstacleIndex;

        public int GetPieceType() {
            return PieceType;
        }

        public int GetScoreValue() {
            return ScoreValue;
        }

        public int GetIndex() {
            return Index;
        }

        public int GetObstacleIndex() {
            return ObstacleIndex;
        }
    }
}