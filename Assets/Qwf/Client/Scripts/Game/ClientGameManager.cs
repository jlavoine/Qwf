using MyLibrary;

namespace Qwf.Client {
    public class ClientGameManager {
        public const int MAX_MOVES_PER_TURN = 3; // TODO constants somewhere?

        private int mMovesMade = 0;

        public ClientGameManager() {
            ListenForMessages( true );
        }

        public void Dispose() {
            ListenForMessages( false );
        }

        private void ListenForMessages( bool i_listen ) {
            if ( i_listen ) {
                MyMessenger.Instance.AddListener( ClientGameEvents.MADE_MOVE, OnMadeMove );
            } else {
                MyMessenger.Instance.RemoveListener( ClientGameEvents.MADE_MOVE, OnMadeMove );
            }
        }

        public void OnMadeMove() {
            mMovesMade++;

            if ( mMovesMade == MAX_MOVES_PER_TURN ) {
                MyMessenger.Instance.Send( ClientGameEvents.MAX_MOVES_MADE );
            }
        }
    }
}