using System;
using MyLibrary;

namespace Qwf.Client {
    public class PassTurnPM : MakeMovePM {
        public PassTurnPM() {
            SetInteractableProperties( true );
            ListenForMessages( true );
        }

        public void Dispose() {
            ListenForMessages( false );
        }

        private void ListenForMessages( bool i_listen ) {
            if ( i_listen ) {
                MyMessenger.Instance.AddListener<IClientMoveAttempt>( ClientGameEvents.MADE_MOVE, OnMadeMove );
                MyMessenger.Instance.AddListener( ClientGameEvents.RESET_MOVES, OnResetMoves );
                MyMessenger.Instance.AddListener<ClientTurnAttempt>( ClientMessages.SEND_TURN_TO_SERVER, OnTurnSent );
            }
            else {
                MyMessenger.Instance.RemoveListener<IClientMoveAttempt>( ClientGameEvents.MADE_MOVE, OnMadeMove );
                MyMessenger.Instance.RemoveListener( ClientGameEvents.RESET_MOVES, OnResetMoves );
                MyMessenger.Instance.RemoveListener<ClientTurnAttempt>( ClientMessages.SEND_TURN_TO_SERVER, OnTurnSent );
            }
        }

        public override void ProcessAction() {
            base.ProcessAction();

            SendEmptyClientTurnAttempt();
        }

        public void OnMadeMove( IClientMoveAttempt i_attempt ) {
            SetInteractableProperties( false );
        }

        public void OnResetMoves() {
            SetInteractableProperties( true );
        }

        public void OnTurnSent( ClientTurnAttempt i_turn ) {
            SetInteractableProperties( true );
        }

        private void SendEmptyClientTurnAttempt() {
            ClientTurnAttempt attempt = new ClientTurnAttempt();
            attempt.PlayerId = BackendManager.Instance.GetPlayerId();
            attempt.MoveAttempts = null;

            MyMessenger.Instance.Send<ClientTurnAttempt>( ClientMessages.SEND_TURN_TO_SERVER, attempt );
        }
    }
}