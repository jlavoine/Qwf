using System;
using MyLibrary;

namespace Qwf.Client {
    public class GamePieceSlotPM : GenericViewModel, IGamePieceSlotPM {
        public const string SLOT_PIECE_TYPE_PROPERTY = "PieceType";
        public const string VISIBLE_PROPERTY = "IsVisible";

        private GamePiecePM mGamePieceInSlot;
        public GamePiecePM GamePieceInSlot { get { return mGamePieceInSlot; } private set { mGamePieceInSlot = value; } }

        private IGamePieceSlot mSlot;
        public IGamePieceSlot Slot { get { return mSlot; } private set { mSlot = value; } }

        public GamePieceSlotPM( IGamePieceSlotUpdate i_data ) {
            SetProperties( i_data );
            CreateGamePieceInSlotPM( i_data );
        }

        public void SetVisibility( bool i_visible ) {
            ViewModel.SetProperty( VISIBLE_PROPERTY, i_visible ? 1f : 0f );
        }

        public void SetProperties( IGamePieceSlotUpdate i_data ) {
            SetSlot( i_data );

            if ( i_data != null ) {
                SetPropertiesForValidPieceUpdate( i_data );
            } else {
                SetPropertiesForMissingPieceUpdate();
            }
        }

        private void SetSlot( IGamePieceSlotUpdate i_data ) {
            Slot = new GamePieceSlot( i_data );
        }

        private void SetPropertiesForValidPieceUpdate( IGamePieceSlotUpdate i_data ) {
            SetSlotPieceTypeProperty( i_data.GetPieceType().ToString() );
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

        private void CreateGamePieceInSlotPM( IGamePieceSlotUpdate i_data ) {
            string playerId = BackendManager.Instance.GetPlayerId();
            GamePieceInSlot = new GamePiecePM( i_data == null ? null : i_data.GetPieceInSlot(), playerId );
        }
    }
}
