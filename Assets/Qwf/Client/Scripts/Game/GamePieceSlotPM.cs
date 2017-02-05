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
            SetSlotPieceTypeProperty( i_data );
            SetVisibility( true );
        }

        private void SetSlotPieceTypeProperty( IGamePieceSlotUpdate i_data ) {
            ViewModel.SetProperty( SLOT_PIECE_TYPE_PROPERTY, i_data.GetSlotPieceType().ToString() );
        }
    }
}
