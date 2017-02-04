﻿using MyLibrary;

namespace Qwf.Client {
    public class GameObstaclePM : GenericViewModel, IGameObstaclePM {
        public const string IMAGE_PROPERTY = "ObstacleImage";
        public const string VISIBLE_PROPERTY = "IsVisible";

        public GameObstaclePM( IGameObstacleUpdate i_data ) {
            SetProperties( i_data );            
        }

        public void SetProperties( IGameObstacleUpdate i_data ) {
            SetImageProperty( i_data );
            SetVisibility( true );
        }

        public void SetVisibility( bool i_visible ) {
            ViewModel.SetProperty( VISIBLE_PROPERTY, i_visible ? 1f : 0f );
        }

        private void SetImageProperty( IGameObstacleUpdate i_data ) {
            ViewModel.SetProperty( IMAGE_PROPERTY, i_data.GetImageKey() );
        }
    }
}
