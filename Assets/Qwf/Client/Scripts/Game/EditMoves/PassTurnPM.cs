using System;
using MyLibrary;

namespace Qwf.Client {
    public class PassTurnPM : GenericViewModel {
        public const string VISIBLE_PROPERTY = "IsVisible";

        public PassTurnPM() {
            SetVisibleProperty( true );
            ListenForMessages( true );
        }

        public void Dispose() {
            ListenForMessages( false );
        }

        private void ListenForMessages( bool i_listen ) {
            if ( i_listen ) {
                MyMessenger.Instance.AddListener( ClientGameEvents.MADE_MOVE, OnMadeMove );
                MyMessenger.Instance.AddListener( ClientGameEvents.RESET_MOVES, OnResetMoves );
            }
            else {
                MyMessenger.Instance.RemoveListener( ClientGameEvents.MADE_MOVE, OnMadeMove );
                MyMessenger.Instance.RemoveListener( ClientGameEvents.RESET_MOVES, OnResetMoves );
            }
        }

        public void OnMadeMove() {

        }

        public void OnResetMoves() {

        }

        private void SetVisibleProperty( bool i_visible ) {
            float fAlpha = i_visible ? 1f : 0f;
            ViewModel.SetProperty( VISIBLE_PROPERTY, fAlpha );
        }
    }
}