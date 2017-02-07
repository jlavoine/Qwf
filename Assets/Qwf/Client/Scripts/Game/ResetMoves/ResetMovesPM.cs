using System;
using MyLibrary;

namespace Qwf.Client {
    public class ResetMovesPM : GenericViewModel {
        public const string IS_VISIBLE_PROPERTY = "IsVisible";

        public ResetMovesPM() {
            ListenForMessages( true );
            SetIsVisibleProperty( false );
        }

        public void Dispose() {
            ListenForMessages( false );
        }

        private void ListenForMessages( bool i_listen ) {
            if ( i_listen ) {
                MyMessenger.Instance.AddListener( ClientGameEvents.MADE_MOVE, OnMadeMove );
            }
            else {
                MyMessenger.Instance.RemoveListener( ClientGameEvents.MADE_MOVE, OnMadeMove );
            }
        }

        public void OnMadeMove() {
            SetIsVisibleProperty( true );
        }

        private void SetIsVisibleProperty( bool i_visible ) {
            float fAlpha = i_visible ? 1f : 0f;
            ViewModel.SetProperty( IS_VISIBLE_PROPERTY, fAlpha );
        }
    }
}