using System.Collections.Generic;

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

        public static List<GamePieceSlotUpdate> GetUpdate( List<IGamePieceSlot> i_slots ) {
            List<GamePieceSlotUpdate> update = new List<GamePieceSlotUpdate>();

            foreach ( IGamePieceSlot slot in i_slots ) {
                GamePieceSlotUpdate slotUpdate = new GamePieceSlotUpdate();
                slotUpdate.SlotPieceType = slot.GetGamePieceType();
                slotUpdate.ScoreValue = slot.GetScoreValue();
                slotUpdate.PieceInSlot = GamePieceData.Create( slot.GetCurrentPiece() );
                update.Add( slotUpdate );
            }

            return update;
        }
    }
}
