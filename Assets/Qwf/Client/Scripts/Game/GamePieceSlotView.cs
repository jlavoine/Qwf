using System;
using MyLibrary;
using UnityEngine.EventSystems;

namespace Qwf.Client {
    public class GamePieceSlotView : GroupView, IDropHandler {
        public GamePieceView GamePieceInSlotView;

        public void Init( GamePieceSlotPM i_pm ) {
            SetModel( i_pm.ViewModel );
            InitGamePieceInSlotView( i_pm );
        }

        private void InitGamePieceInSlotView( GamePieceSlotPM i_pm ) {
            GamePieceInSlotView.Init( i_pm.GamePieceInSlot );
        }

        public void OnDrop( PointerEventData eventData ) {
            UnityEngine.Debug.LogError( "Dropped on me" );
        }
    }
}