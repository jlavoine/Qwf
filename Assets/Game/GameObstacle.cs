using System.Collections.Generic;

namespace Qwf {
    public class GameObstacle : IGameObstacle {
        private GameObstacleData mData;
        private List<IGamePieceSlot> mSlots;

        public GameObstacle( GameObstacleData i_data ) {
            mData = i_data;

            CreateSlots();
        }

        public GameObstacle( List<IGamePieceSlot> i_slots ) {
            mSlots = i_slots;
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

        public bool CanPieceBePlacedIntoSlot( IGamePiece i_piece, IGamePieceSlot i_slot ) {
            if ( DoesObstacleHaveSlot( i_slot ) ) {
                return i_slot.CanPlacePieceIntoSlot( i_piece );
            } else {
                return false;
            }
        }

        public bool DoesObstacleHaveSlot( IGamePieceSlot i_slot ) {
            return mSlots.Contains( i_slot );
        }

        public bool IsComplete() {
            foreach ( IGamePieceSlot slot in mSlots ) {
                if ( slot.IsEmpty() ) {
                    return false;
                }
            }

            return true;
        }

        public void Score( IScoreKeeper i_scoreKeeper ) {

        }
    }
}
