using UnityEngine;
using MyLibrary;
using UnityEngine.EventSystems;

namespace Qwf.Client {
    public class GamePieceSlotView : GroupView, IDropHandler {
        private GamePieceSlotPM mPM;

        public GamePieceView GamePieceInSlotView;

        public void Init( GamePieceSlotPM i_pm ) {
            mPM = i_pm;
            SetModel( i_pm.ViewModel );
            InitGamePieceInSlotView( i_pm );
        }

        private void InitGamePieceInSlotView( GamePieceSlotPM i_pm ) {
            GamePieceInSlotView.Init( i_pm.GamePieceInSlot );
        }

        public void OnDrop( PointerEventData i_eventData ) {
            GameObject droppedObject = i_eventData.pointerDrag;
            PlayerHandGamePieceView pieceView = droppedObject.GetComponent<PlayerHandGamePieceView>();
            if ( pieceView != null ) {                
                IPlayerHandGamePiecePM piecePM = pieceView.PM;
                mPM.AttemptToPlayPieceInSlot( pieceView.PM );
                UnityEngine.Debug.LogError( "Can the incoming piece be placed: " + mPM.Slot.CanPlacePieceIntoSlot( piecePM.GamePiece ) );
            }
        }
    }
}