using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace Qwf.Client {
    public class PlayerHandGamePieceView : GamePieceView, IBeginDragHandler, IDragHandler, IEndDragHandler {
        public CanvasGroup CanvasGroup;

        private Vector3 mStartPosition;

        public void OnBeginDrag( PointerEventData i_eventData ) {
            BlockRaycasts( false );
            SaveStartPosition();
        }

        public void OnDrag( PointerEventData i_eventData ) {
            UpdatePosition( i_eventData );
        }

        public void OnEndDrag( PointerEventData i_eventData ) {
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
