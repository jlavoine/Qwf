using MyLibrary;
using UnityEngine;

namespace Qwf.Client {
    public class GamePiecePM : GenericViewModel {
        public const string VALUE_PROPERTY = "Value";
        public const string ICON_PROPERTY = "PieceType";
        public const string OUTLINE_PROPERTY = "OutlineColor";

        private IGamePiece mGamePiece;
        private string mPlayerViewing;

        public GamePiecePM( IGamePiece i_piece, string i_playerViewing ) {
            mPlayerViewing = i_playerViewing;
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
            if ( DoesViewingPlayerOwnGamePiece() ) {
                ViewModel.SetProperty( OUTLINE_PROPERTY, new Color( 0, 0, 255 ) );  // TODO replace these with constants
            } else {
                ViewModel.SetProperty( OUTLINE_PROPERTY, new Color( 255, 0, 0 ) );
            }
        }

        private bool DoesViewingPlayerOwnGamePiece() {
            return mPlayerViewing == mGamePiece.GetOwner().Id;
        }
    }
}