using MyLibrary;

namespace Qwf.Client {
    public class GameOverPM : GenericViewModel {
        public const string VISIBLE_PROPERTY = "IsVisible";
        public const string BODY_TEXT_PROPERTY = "GameOverBody";

        public const string LOST_GAME_KEY = "GameOver_Lost";
        public const string WON_GAME_KEY = "GameOver_Won";

        public GameOverPM() {
            SetVisibleProperty( false );
            ListenForMessages( true );
        }

        public void Dispose() {
            ListenForMessages( false );
        }

        private void ListenForMessages( bool i_listen ) {
            if ( i_listen ) {
                MyMessenger.Instance.AddListener<IGameOverUpdate>( ClientMessages.GAME_OVER_UPDATE, OnGameOver );
            }
            else {
                MyMessenger.Instance.RemoveListener<IGameOverUpdate>( ClientMessages.GAME_OVER_UPDATE, OnGameOver );
            }
        }

        public void OnGameOver( IGameOverUpdate i_update ) {
            SetVisibleProperty( true );
            SetBodyTextProperty( i_update.DidClientWin() );
        }

        private void SetBodyTextProperty( bool i_won ) {
            string text = StringTableManager.Instance.Get( i_won ? WON_GAME_KEY : LOST_GAME_KEY );
            ViewModel.SetProperty( BODY_TEXT_PROPERTY, text );
        }

        private void SetVisibleProperty( bool i_visible ) {
            ViewModel.SetProperty( VISIBLE_PROPERTY, i_visible );
        }
    }
}
