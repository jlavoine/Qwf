
namespace Qwf {
    public class GamePieceSlotData : IGamePieceSlotData {
        public int PieceType;
        public int ScoreValue;

        public int GetPieceType() {
            return PieceType;
        }

        public int GetScoreValue() {
            return ScoreValue;
        }
    }
}