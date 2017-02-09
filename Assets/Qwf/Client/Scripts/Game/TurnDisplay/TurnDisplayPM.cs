using MyLibrary;

namespace Qwf.Client {
    public class TurnDisplayPM : GenericViewModel {

        public TurnDisplayPM() {
            ListenForMessages( true );
        }

        public void Dispose() {
            ListenForMessages( false );
        }

        private void ListenForMessages( bool i_listen ) {
            if ( i_listen ) {
               // MyMessenger.Instance.AddListener( ClientGameEvents.MAX_MOVES_MADE, OnMaxMovesMade );
               // MyMessenger.Instance.AddListener( ClientGameEvents.RESET_MOVES, OnMovesReset );
            }
            else {
               // MyMessenger.Instance.RemoveListener( ClientGameEvents.MAX_MOVES_MADE, OnMaxMovesMade );
               // MyMessenger.Instance.RemoveListener( ClientGameEvents.RESET_MOVES, OnMovesReset );
            }
        }
    }
}
