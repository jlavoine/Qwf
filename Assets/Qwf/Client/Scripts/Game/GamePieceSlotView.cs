using System;
using MyLibrary;

namespace Qwf.Client {
    public class GamePieceSlotView : GroupView {
        public GamePieceView GamePieceInSlotView;

        public void Init( GamePieceSlotPM i_pm ) {
            SetModel( i_pm.ViewModel );
            InitGamePieceInSlotView( i_pm );
        }

        private void InitGamePieceInSlotView( GamePieceSlotPM i_pm ) {
            GamePieceInSlotView.Init( i_pm.GamePieceInSlot );
        }
    }
}