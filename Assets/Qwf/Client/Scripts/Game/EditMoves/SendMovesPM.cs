﻿using MyLibrary;
using System.Collections.Generic;

namespace Qwf.Client {
    public class SendMovesPM : MakeMovePM {
        private List<IClientMoveAttempt> mMoveAttempts = new List<IClientMoveAttempt>();
        public List<IClientMoveAttempt> MoveAttempts { get { return mMoveAttempts; } set { mMoveAttempts = value; } }

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

            SendClientTurnAttempt();
            ResetMoveAttemptsList();
            SetInteractableProperties( false );
        }

        public void OnMadeMove( IClientMoveAttempt i_attempt ) {
            SetInteractableProperties( true );
            AddMoveAttemptToList( i_attempt );
        }

        public void OnResetMoves() {
            SetInteractableProperties( false );
            ResetMoveAttemptsList();
        }

        private void ResetMoveAttemptsList() {
            MoveAttempts = new List<IClientMoveAttempt>();
        }

        private void AddMoveAttemptToList( IClientMoveAttempt i_attempt ) {
            MoveAttempts.Add( i_attempt );
        }

        private void SendClientTurnAttempt() {
            ClientTurnAttempt turnAttempt = new ClientTurnAttempt();
            turnAttempt.PlayerId = BackendManager.Instance.GetPlayerId();
            turnAttempt.MoveAttempts = new List<ClientMoveAttempt>();

            foreach ( IClientMoveAttempt moveAttempt in MoveAttempts ) {
                turnAttempt.MoveAttempts.Add( (ClientMoveAttempt) moveAttempt );
            }

            MyMessenger.Instance.Send<ClientTurnAttempt>( ClientMessages.SEND_TURN_TO_SERVER, turnAttempt );
        }
    }
}
