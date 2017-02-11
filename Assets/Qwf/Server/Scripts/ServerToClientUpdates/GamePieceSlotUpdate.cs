using System.Collections.Generic;

namespace Qwf {
    public class GamePieceSlotUpdate : IGamePieceSlotUpdate {
        public int SlotPieceType;
        public int ScoreValue;
        public int Index;
        public int ObstacleIndex;
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

        public int GetObstacleIndex() {
            return ObstacleIndex;
        }

        public int GetIndex() {
            return Index;
        }

        public static List<GamePieceSlotUpdate> Create( List<IGamePieceSlot> i_slots, GameObstacleUpdate i_obstacleUpdate ) {
            List<GamePieceSlotUpdate> update = new List<GamePieceSlotUpdate>();

            int index = 0;
            foreach ( IGamePieceSlot slot in i_slots ) {
                GamePieceSlotUpdate slotUpdate = new GamePieceSlotUpdate();
                slotUpdate.SlotPieceType = slot.GetGamePieceType();
                slotUpdate.ScoreValue = slot.GetScoreValue();
                slotUpdate.Index = index;
                slotUpdate.ObstacleIndex = i_obstacleUpdate.Index;
                slotUpdate.PieceInSlot = GamePieceData.Create( slot.GetCurrentPiece() );
                update.Add( slotUpdate );

                index++;
            }

            return update;
        }
    }
}
