
namespace Qwf {
    public class ClientMoveAttempt : IClientMoveAttempt {
        public int PieceInHandIndex;
        public int ObstacleIndex;
        public int ObstacleSlotIndex;

        public int GetPlayerPieceIndex() {
            return PieceInHandIndex;
        }

        public int GetObstacleIndex() {
            return ObstacleIndex;
        }

        public int GetSlotIndex() {
            return ObstacleSlotIndex;
        }
    }
}
