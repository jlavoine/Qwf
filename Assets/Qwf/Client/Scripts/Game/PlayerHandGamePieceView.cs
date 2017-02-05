using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace Qwf.Client {
    public class PlayerHandGamePieceView : GamePieceView, IBeginDragHandler, IDragHandler, IEndDragHandler {
        public CanvasGroup CanvasGroup;

        private Vector3 mStartPosition;

        public void OnBeginDrag( PointerEventData eventData ) {
            BlockRaycasts( false );
            SaveStartPosition();
        }

        public void OnDrag( PointerEventData eventData ) {
            UpdatePosition( eventData );
        }

        public void OnEndDrag( PointerEventData eventData ) {
            BlockRaycasts( true );
            ResetPosition();
        }

        private void BlockRaycasts( bool i_block ) {
            CanvasGroup.blocksRaycasts = i_block;
        }

        private void SaveStartPosition() {
            mStartPosition = transform.position;
        }

        private void UpdatePosition( PointerEventData i_eventData ) {
            transform.position = i_eventData.position;
        }

        private void ResetPosition() {
            transform.position = mStartPosition;
        }
    }
}
