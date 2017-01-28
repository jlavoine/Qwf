using MyLibrary;

namespace Qwf.Client {
    public class GamePiecePM : GenericViewModel {
        public const string VALUE_PROPERTY = "Value";
        public const string ICON_PROPERTY = "PieceType";

        private IGamePiece mGamePiece;

        public GamePiecePM( IGamePiece i_piece ) {
            mGamePiece = i_piece;

            SetValueProperty();
            SetIconProperty();
            SetOutlineProperty();
        }

        private void SetValueProperty() {
            ViewModel.SetProperty( VALUE_PROPERTY, mGamePiece.GetValue() );
        }

        private void SetIconProperty() {
            ViewModel.SetProperty( ICON_PROPERTY, mGamePiece.GetPieceType().ToString() );
        }

        private void SetOutlineProperty() {

        }
    }
}