using System.Collections.Generic;

namespace Qwf {
    public class GameObstacle : IGameObstacle {
        private List<IGamePieceSlot> mSlots;

        private int mScoreValue;

        public GameObstacle( List<IGamePieceSlot> i_slots, int i_scoreValue ) {
            mScoreValue = i_scoreValue;
            mSlots = i_slots;
        }

        public List<IGamePieceSlot> GetSlots() {
            return mSlots;
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
            i_scoreKeeper.AddPointsToPlayer( i_player, mScoreValue );
        }

        private void ScoreAllSlots( IScoreKeeper i_scoreKeeper ) {
            foreach ( IGamePieceSlot slot in mSlots ) {
                slot.Score( i_scoreKeeper );
            }
        }
    }
}
