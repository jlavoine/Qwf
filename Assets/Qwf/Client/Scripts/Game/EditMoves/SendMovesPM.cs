﻿using MyLibrary;

namespace Qwf.Client {
    public class SendMovesPM : MakeMovePM {
        public SendMovesPM() {
            SetInteractableProperties( false );
            ListenForMessages( true );
        }

        public void Dispose() {
            ListenForMessages( false );
        }

        private void ListenForMessages( bool i_listen ) {
            if ( i_listen ) {
                MyMessenger.Instance.AddListener<IClientMoveAttempt>( ClientGameEvents.MADE_MOVE, OnMadeMove );
                MyMessenger.Instance.AddListener( ClientGameEvents.RESET_MOVES, OnResetMoves );
            }
            else {
                MyMessenger.Instance.RemoveListener<IClientMoveAttempt>( ClientGameEvents.MADE_MOVE, OnMadeMove );
                MyMessenger.Instance.RemoveListener( ClientGameEvents.RESET_MOVES, OnResetMoves );
            }
        }

        public override void ProcessAction() {
            base.ProcessAction();
        }

        public void OnMadeMove( IClientMoveAttempt i_attempt ) {
            SetInteractableProperties( true );
        }

        public void OnResetMoves() {
            SetInteractableProperties( false );
        }

        private void SendEmptyClientTurnAttempt() {
            ClientTurnAttempt attempt = new ClientTurnAttempt();
            attempt.PlayerId = BackendManager.Instance.GetPlayerId();
            attempt.MoveAttempts = null;

            MyMessenger.Instance.Send<ClientTurnAttempt>( ClientMessages.SEND_TURN_TO_SERVER, attempt );
        }
    }
}
