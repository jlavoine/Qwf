
namespace Qwf {
    public class GamePieceSlotUpdate : IGamePieceSlotUpdate {
        public int SlotPieceType;
        public int ScoreValue;
        public GamePieceData PieceInSlot;

        public int GetSlotPieceType() {
            return SlotPieceType;
        }

        public GamePieceData GetPieceInSlot() {
            return PieceInSlot;
        }

        public int GetPieceType() {
            return SlotPieceType;
        }

        public int GetScoreValue() {
            return ScoreValue;
        }
    }
}
