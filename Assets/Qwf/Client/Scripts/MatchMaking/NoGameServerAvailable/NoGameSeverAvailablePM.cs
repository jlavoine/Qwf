using MyLibrary;

namespace Qwf.Client {
    public class NoGameSeverAvailablePM : GenericViewModel {
        public const string VISIBLE_PROPERTY = "IsVisible";
        public const string TEXT_PROPERTY = "Text";

        public const string NO_GAME_SERVER_STRING_KEY = "NoGameServersAvailable";

        public NoGameSeverAvailablePM() {
            ListenForMessages( true );
            SetVisibleProperty( false );
            SetTextProperty( NO_GAME_SERVER_STRING_KEY );
        }

        public void Dispose() {
            ListenForMessages( false );
        }

        private void ListenForMessages( bool i_listen ) {
            if ( i_listen ) {
                MyMessenger.Instance.AddListener( ClientMessages.NO_SERVER_AVAILABLE, OnNoServerAvailable );
            }
            else {
                MyMessenger.Instance.RemoveListener( ClientMessages.NO_SERVER_AVAILABLE, OnNoServerAvailable );
            }
        }

        public void OnNoServerAvailable() {
            SetVisibleProperty( true );
        }

        private void SetVisibleProperty( bool i_visible ) {
            ViewModel.SetProperty( VISIBLE_PROPERTY, i_visible );
        }

        private void SetTextProperty( string i_key ) {
            string text = StringTableManager.Instance.Get( i_key );
            ViewModel.SetProperty( TEXT_PROPERTY, text );
        }
    }
}
