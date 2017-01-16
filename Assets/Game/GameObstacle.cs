using System.Collections.Generic;

namespace Qwf {
    public class GameObstacle : IGameObstacle {
        private GameObstacleData mData;
        private List<IGamePieceSlot> mSlots;

        public GameObstacle( GameObstacleData i_data ) {
            mData = i_data;

            CreateSlots();
        }

        private void CreateSlots() {
            mSlots = new List<IGamePieceSlot>();
            foreach ( GamePieceSlotData slotData in mData.SlotsData ) {
                mSlots.Add( new GamePieceSlot( slotData ) );
            }
        }

        public List<IGamePieceSlot> GetSlots() {
            return mSlots;
        }

        public GameObstacleData GetData() {
            return mData;
        }
    }
}
