using UnityEngine;
using MyLibrary;
using UnityEngine.EventSystems;

namespace Qwf.Client {
    public class GamePieceSlotView : GroupView {
        private GamePieceSlotPM mPM;
        public GamePieceSlotPM PM { get { return mPM; } }

        public GamePieceView GamePieceInSlotView;

        public void Init( GamePieceSlotPM i_pm ) {
            mPM = i_pm;
            SetModel( i_pm.ViewModel );
            InitGamePieceInSlotView( i_pm );
        }

        private void InitGamePieceInSlotView( GamePieceSlotPM i_pm ) {
            GamePieceInSlotView.Init( i_pm.GamePieceInSlot );
        }
    }
}