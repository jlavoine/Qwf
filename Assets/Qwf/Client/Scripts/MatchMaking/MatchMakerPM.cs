using MyLibrary;

namespace Qwf.Client {
    public class MatchMakerPM : GenericViewModel {
        public const string STATUS_PROPERTY = "MatchStatus";
        public const string VISIBLE_PROPERTY = "IsVisible";

        public const string SEARCHING_KEY = "MatchMaking_Searching";

        public MatchMakerPM() {
            SetVisibleProperty( true );
            SetStatusProperty( SEARCHING_KEY );
            ListenForMessages( true );
        }

        public void Dispose() {
            ListenForMessages( false );
        }

        private void ListenForMessages( bool i_listen ) {
            if ( i_listen ) {
                MyMessenger.Instance.AddListener( ClientMessages.GAME_READY, OnGameReady );
            }
            else {
                MyMessenger.Instance.RemoveListener( ClientMessages.GAME_READY, OnGameReady );
            }
        }

        public void OnGameReady() {
            SetVisibleProperty( false );
        }

        private void SetStatusProperty( string i_key ) {
            string text = StringTableManager.Instance.Get( i_key );
            ViewModel.SetProperty( STATUS_PROPERTY, text );
        }

        private void SetVisibleProperty( bool i_visible ) {
            ViewModel.SetProperty( VISIBLE_PROPERTY, i_visible );
        }
    }
}
