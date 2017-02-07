using MyLibrary;

namespace Qwf.Client {
    public class PassTurnPM : GenericViewModel {
        public const string VISIBLE_PROPERTY = "IsVisible";

        public PassTurnPM() {
            SetVisibleProperty( true );
        }

        public void Dispose() {

        }

        private void SetVisibleProperty( bool i_visible ) {
            float fAlpha = i_visible ? 1f : 0f;
            ViewModel.SetProperty( VISIBLE_PROPERTY, fAlpha );
        }
    }
}