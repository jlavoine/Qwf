using System;
using MyLibrary;

namespace Qwf.Client {
    public class GamePieceSlotPM : GenericViewModel, IGamePieceSlotPM {
        public const string SLOT_PIECE_TYPE_PROPERTY = "PieceType";
        public const string VISIBLE_PROPERTY = "IsVisible";

        public GamePieceSlotPM( IGamePieceSlotUpdate i_data ) {
            SetProperties( i_data );
        }

        public void SetVisibility( bool i_visible ) {
            ViewModel.SetProperty( VISIBLE_PROPERTY, i_visible ? 1f : 0f );
        }

        public void SetProperties( IGamePieceSlotUpdate i_data ) {
            if ( i_data != null ) {
                SetPropertiesForValidPieceUpdate( i_data );
            } else {
                SetPropertiesForMissingPieceUpdate();
            }
        }

        private void SetPropertiesForValidPieceUpdate( IGamePieceSlotUpdate i_data ) {
            SetSlotPieceTypeProperty( i_data.GetSlotPieceType().ToString() );
            SetVisibility( true );
        }

        private void SetPropertiesForMissingPieceUpdate() {
            // this is actually valid -- it just means there's no piece slot
            SetSlotPieceTypeProperty( "0" );
            SetVisibility( false );            
        }

        private void SetSlotPieceTypeProperty( string i_pieceType ) {
            ViewModel.SetProperty( SLOT_PIECE_TYPE_PROPERTY, i_pieceType );
        }
    }
}
