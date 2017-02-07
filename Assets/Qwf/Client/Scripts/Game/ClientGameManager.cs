using MyLibrary;

namespace Qwf.Client {
    public class ClientGameManager {
        public const int MAX_MOVES_PER_TURN = 3; // TODO constants somewhere?

        private int mMovesMade = 0;
        public int MovesMade { get { return mMovesMade; } set { mMovesMade = value; } }

        public ClientGameManager() {
            ListenForMessages( true );
        }

        public void Dispose() {
            ListenForMessages( false );
        }

        private void ListenForMessages( bool i_listen ) {
            if ( i_listen ) {
                MyMessenger.Instance.AddListener( ClientGameEvents.MADE_MOVE, OnMadeMove );
                MyMessenger.Instance.AddListener( ClientGameEvents.RESET_MOVES, OnResetMoves );
            } else {
                MyMessenger.Instance.RemoveListener( ClientGameEvents.MADE_MOVE, OnMadeMove );
                MyMessenger.Instance.RemoveListener( ClientGameEvents.RESET_MOVES, OnResetMoves );
            }
        }

        public void OnMadeMove() {
            MovesMade++;

            if ( mMovesMade == MAX_MOVES_PER_TURN ) {
                MyMessenger.Instance.Send( ClientGameEvents.MAX_MOVES_MADE );
            }
        }

        public void OnResetMoves() {
            MovesMade = 0;
        }
    }
}