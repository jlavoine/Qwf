using MyLibrary;
using UnityEngine;

namespace Qwf.Client {
    public class GamePiecePM : GenericViewModel {
        public const string VALUE_PROPERTY = "Value";
        public const string ICON_PROPERTY = "PieceType";
        public const string OUTLINE_PROPERTY = "OutlineColor";
        public const string VISIBLE_PROPERTY = "IsVisible";

        private GamePieceData mGamePiece;
        private string mPlayerViewing;

        public GamePiecePM( GamePieceData i_piece, string i_playerViewing ) {
            mPlayerViewing = i_playerViewing;
            mGamePiece = i_piece;

            SetProperties( i_piece );   
        }

        public void SetProperties( GamePieceData i_piece ) {
            mGamePiece = i_piece;

            SetValueProperty();
            SetIconProperty();
            SetOutlineProperty();
            SetVisibility( true );
        }

        public void SetVisibility( bool i_visible) {
            ViewModel.SetProperty( VISIBLE_PROPERTY, i_visible ? 1f : 0f );
        }

        private void SetValueProperty() {
            ViewModel.SetProperty( VALUE_PROPERTY, mGamePiece.Value );
        }

        private void SetIconProperty() {
            ViewModel.SetProperty( ICON_PROPERTY, mGamePiece.PieceType.ToString() );
        }

        private void SetOutlineProperty() {
            if ( DoesViewingPlayerOwnGamePiece() ) {
                ViewModel.SetProperty( OUTLINE_PROPERTY, new Color( 0, 0, 255 ) );  // TODO replace these with constants
            } else {
                ViewModel.SetProperty( OUTLINE_PROPERTY, new Color( 255, 0, 0 ) );
            }
        }

        private bool DoesViewingPlayerOwnGamePiece() {
            return mPlayerViewing == mGamePiece.Owner;
        }
    }
}