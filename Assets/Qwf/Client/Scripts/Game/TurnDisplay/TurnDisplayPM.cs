using MyLibrary;

namespace Qwf.Client {
    public class TurnDisplayPM : GenericViewModel {

        public const string PLAYER_TURN_MESSAGE = "Your Turn";
        public const string OPPONENT_TURN_MESSAGE = "Their Turn";

        public const string DISPLAY_PROPERTY = "TurnDisplayText";

        public TurnDisplayPM() {
            ListenForMessages( true );
        }

        public void Dispose() {
            ListenForMessages( false );
        }

        private void ListenForMessages( bool i_listen ) {
            if ( i_listen ) {
                MyMessenger.Instance.AddListener<ITurnUpdate>( ClientMessages.UPDATE_TURN, OnTurnUpdate );               
            }
            else {
                MyMessenger.Instance.RemoveListener<ITurnUpdate>( ClientMessages.UPDATE_TURN, OnTurnUpdate );
            }
        }

        public void OnTurnUpdate( ITurnUpdate i_update ) {
            if ( IsActivePlayersTurn( i_update ) ) {
                SetDisplayProperty( PLAYER_TURN_MESSAGE );
            } else {
                SetDisplayProperty( OPPONENT_TURN_MESSAGE );
            }
        }

        private bool IsActivePlayersTurn( ITurnUpdate i_update ) {
            return i_update.IsThisPlayerActive();
        }

        private void SetDisplayProperty( string i_message ) {
            ViewModel.SetProperty( DISPLAY_PROPERTY, i_message );
        }
    }
}
