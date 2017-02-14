using MyLibrary;

namespace Qwf.Client {
    public class GameOverPM : GenericViewModel {
        public const string VISIBLE_PROPERTY = "IsVisible";
        public const string BODY_TEXT_PROPERTY = "GameOverBody";

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
        }

        private void SetVisibleProperty( bool i_visible ) {
            ViewModel.SetProperty( VISIBLE_PROPERTY, i_visible );
        }
    }
}
