using System;
using MyLibrary;

namespace Qwf.Client {
    public class PassTurnPM : MakeMovePM {
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

        public override void ProcessAction() {
            base.ProcessAction();

            SendEmptyClientTurnAttempt();
        }

        public void OnMadeMove() {
            SetVisibleProperty( false );
        }

        public void OnResetMoves() {
            SetVisibleProperty( true );
        }

        private void SetVisibleProperty( bool i_visible ) {
            float fAlpha = i_visible ? 1f : 0f;
            ViewModel.SetProperty( VISIBLE_PROPERTY, fAlpha );
        }

        private void SendEmptyClientTurnAttempt() {
            ClientTurnAttempt attempt = new ClientTurnAttempt();
            attempt.PlayerId = BackendManager.Instance.GetPlayerId();
            attempt.MoveAttempts = null;

            MyMessenger.Instance.Send<ClientTurnAttempt>( ClientMessages.SEND_TURN_TO_SERVER, attempt );
        }
    }
}