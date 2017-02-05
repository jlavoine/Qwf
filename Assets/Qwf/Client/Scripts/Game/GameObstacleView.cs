using System.Collections.Generic;
using MyLibrary;

namespace Qwf.Client {
    public class GameObstacleView : GroupView {
        public List<GamePieceSlotView> SlotViews;

        public void Init( GameObstaclePM i_pm ) {
            SetModel( i_pm.ViewModel );
            InitAllGamePieceSlots( i_pm );
        }

        private void InitAllGamePieceSlots( GameObstaclePM i_pm ) {
            for ( int i = 0; i < SlotViews.Count; ++i ) {
                SlotViews[i].Init( i_pm.SlotPiecePMs[i] );
            }
        }
    }
}