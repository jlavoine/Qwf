using System;
using System.Collections.Generic;

namespace Qwf {
    public class GameObstacleUpdate : IGameObstacleUpdate {
        public const string IMAGE_PREFIX = "Obstacle_";

        public string Id;
        public int FinalBlowValue;
        public int Index;
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

        public int GetIndex() {
            return Index;
        }

        public IGamePieceSlotUpdate GetSlotUpdate( int i_index ) {
            return PieceSlots[i_index];
        }

        public static GameObstacleUpdate Create( IGameObstacle i_obstacle, int i_index ) {
            GameObstacleUpdate update = new GameObstacleUpdate();
            update.FinalBlowValue = i_obstacle.GetFinalBlowValue();
            update.Id = i_obstacle.GetId();
            update.Index = i_index;

            update.PieceSlots = GamePieceSlotUpdate.Create( i_obstacle.GetSlots(), update );

            return update;
        }
    }
}