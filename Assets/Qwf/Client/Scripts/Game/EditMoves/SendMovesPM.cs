using MyLibrary;

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
        }

        public void OnMadeMove() {
            SetInteractableProperties( true );
        }

        public void OnResetMoves() {
            SetInteractableProperties( false );
        }
    }
}
