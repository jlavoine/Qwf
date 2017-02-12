using System.Collections.Generic;

namespace Qwf {
    public class GameObstacle : IGameObstacle {
        private List<IGamePieceSlot> mSlots;

        private IGameObstacleData mData;

        public GameObstacle( List<IGamePieceSlot> i_slots, IGameObstacleData i_data ) {
            mData = i_data;
            mSlots = i_slots;
        }

        public string GetId() {
            return mData.GetId();
        }

        public int GetFinalBlowValue() {
            return mData.GetFinalBlowValue();
        }

        public List<IGamePieceSlot> GetSlots() {
            return mSlots;
        }

        public IGamePieceSlot GetSlotOfIndex( int i_index ) {
            if ( i_index < 0 || i_index >= mSlots.Count ) {
                return null;
            } else {
                return mSlots[i_index];
            }
        }

        public bool CanPieceBePlacedIntoSlot( IServerGamePiece i_piece, IGamePieceSlot i_slot ) {
            if ( DoesObstacleHaveSlot( i_slot ) ) {
                return i_slot.CanPlacePieceIntoSlot( i_piece );
            } else {
                return false;
            }
        }

        public bool DoesObstacleHaveSlot( IGamePieceSlot i_slot ) {
            return mSlots.Contains( i_slot );
        }

        public bool CanScore() {
            return IsComplete();
        }

        public bool IsComplete() {
            foreach ( IGamePieceSlot slot in mSlots ) {
                if ( slot.IsEmpty() ) {
                    return false;
                }
            }

            return true;
        }

        public void Score( IScoreKeeper i_scoreKeeper, IGamePlayer i_currentPlayer ) {            
            AwardPointsToCurrentPlayer( i_scoreKeeper, i_currentPlayer );
            ScoreAllSlots( i_scoreKeeper );
        }

        private void AwardPointsToCurrentPlayer( IScoreKeeper i_scoreKeeper, IGamePlayer i_player ) {
            i_scoreKeeper.AddPointsToPlayer( i_player, GetFinalBlowValue() );
        }

        private void ScoreAllSlots( IScoreKeeper i_scoreKeeper ) {
            foreach ( IGamePieceSlot slot in mSlots ) {
                slot.Score( i_scoreKeeper );
            }
        }
    }
}
