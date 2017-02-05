
namespace Qwf {
    public class GamePieceSlotUpdate : IGamePieceSlotUpdate {
        public int SlotPieceType;
        public GamePieceData PieceInSlot;

        public int GetSlotPieceType() {
            return SlotPieceType;
        }

        public GamePieceData GetPieceInSlot() {
            return PieceInSlot;
        }
    }
}
