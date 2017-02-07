using MyLibrary;

namespace Qwf.Client {
    public class SendMovesPM : GenericViewModel {
        public const string VISIBLE_PROPERTY = "IsVisible";

        public SendMovesPM() {
            SetVisibleProperty( false );
        }

        public void Dispose() {

        }

        private void SetVisibleProperty( bool i_visible ) {
            float fAlpha = i_visible ? 1f : 0f;
            ViewModel.SetProperty( VISIBLE_PROPERTY, fAlpha );
        }
    }
}
