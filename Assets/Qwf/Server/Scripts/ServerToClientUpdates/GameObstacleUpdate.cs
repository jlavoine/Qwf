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

        public static GameObstacleUpdate GetUpdate( IGameObstacle i_obstacle ) {
            GameObstacleUpdate update = new GameObstacleUpdate();
            update.FinalBlowValue = i_obstacle.GetFinalBlowValue();
            update.Id = i_obstacle.GetId();

            update.PieceSlots = GamePieceSlotUpdate.GetUpdate( i_obstacle.GetSlots() );

            return update;
        }
    }
}