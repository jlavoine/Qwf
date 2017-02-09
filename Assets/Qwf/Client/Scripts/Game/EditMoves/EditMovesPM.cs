using MyLibrary;

namespace Qwf.Client {
    public class EditMovesPM :GenericViewModel  {
        public const string VISIBLE_PROPERTY = "IsVisible";

        public EditMovesPM() {
            ListenForMessages( true );
            SetVisibleProperty( false );
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
            if ( i_update.IsThisPlayerActive() ) {
                SetVisibleProperty( true );
            } else {
                SetVisibleProperty( false );
            }
        }

        private void SetVisibleProperty( bool i_visible ) {
            float fAlpha = i_visible ? 1f : 0f;
            ViewModel.SetProperty( VISIBLE_PROPERTY, fAlpha );
        }
    }
}
