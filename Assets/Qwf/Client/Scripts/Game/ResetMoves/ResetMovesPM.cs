using System;
using MyLibrary;

namespace Qwf.Client {
    public class ResetMovesPM : GenericViewModel {
        public const string IS_VISIBLE_PROPERTY = "IsVisible";

        private IGameObstaclesUpdate mUpdate;
        public IGameObstaclesUpdate CachedUpdate { get { return mUpdate; } set { mUpdate = value; } }

        public ResetMovesPM() {
            ListenForMessages( true );
            SetIsVisibleProperty( false );
        }

        public void Dispose() {
            ListenForMessages( false );
        }

        private void ListenForMessages( bool i_listen ) {
            if ( i_listen ) {
                MyMessenger.Instance.AddListener( ClientGameEvents.MADE_MOVE, OnMadeMove );
                MyMessenger.Instance.AddListener<IGameObstaclesUpdate>( ClientMessages.UPDATE_OBSTACLES, OnUpdateObstacles );
            }
            else {
                MyMessenger.Instance.RemoveListener( ClientGameEvents.MADE_MOVE, OnMadeMove );
                MyMessenger.Instance.RemoveListener<IGameObstaclesUpdate>( ClientMessages.UPDATE_OBSTACLES, OnUpdateObstacles );
            }
        }

        public void OnMadeMove() {
            SetIsVisibleProperty( true );
        }

        public void ResetMoves() {
            MyMessenger.Instance.Send( ClientGameEvents.RESET_MOVES );
            MyMessenger.Instance.Send( ClientMessages.UPDATE_OBSTACLES, CachedUpdate );
            SetIsVisibleProperty( false );
        }

        public void OnUpdateObstacles( IGameObstaclesUpdate i_update ) {
            CachedUpdate = i_update;
        }

        private void SetIsVisibleProperty( bool i_visible ) {
            float fAlpha = i_visible ? 1f : 0f;
            ViewModel.SetProperty( IS_VISIBLE_PROPERTY, fAlpha );
        }
    }
}