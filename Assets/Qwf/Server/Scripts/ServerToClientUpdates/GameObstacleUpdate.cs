using System;
using System.Collections.Generic;

namespace Qwf {
    public class GameObstacleUpdate : IGameObstacleUpdate {
        public const string IMAGE_PREFIX = "Obstacle_";

        public string Id;
        public int FinalBlowValue;
        public List<GamePieceSlotUpdate> PieceSlots;

        public string GetId() {
            return Id;
        }

        public string GetImageKey() {
            return IMAGE_PREFIX + GetId();
        }

        public int GetFinalBlowValue() {
            return FinalBlowValue;
        }

        public int GetSlotCount() {
            return PieceSlots.Count;
        }

        public IGamePieceSlotUpdate GetSlotUpdate( int i_index ) {
            return PieceSlots[i_index];
        }
    }
}