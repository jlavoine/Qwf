using System.Collections.Generic;

namespace Qwf {
    public class GameObstacleData : IGameObstacleData {
        public string Id;
        public int FinalBlowValue;
        public List<GamePieceSlotData> SlotsData;

        public string GetId() {
            return Id;
        }

        public int GetFinalBlowValue() {
            return FinalBlowValue;
        }
    }
}