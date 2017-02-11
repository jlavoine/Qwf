using System;
using MyLibrary;

namespace Qwf.Client {
    public class ResetMovesPM : GenericViewModel {
        public const string VISIBLE_PROPERTY = "IsVisible";
        public const string USE_PROPERTY = "CanUse";

        private IGameObstaclesUpdate mUpdate;
        public IGameObstaclesUpdate CachedUpdate { get { return mUpdate; } set { mUpdate = value; } }

        public ResetMovesPM() {
            ListenForMessages( true );
            SetInteractableProperties( false );
        }

        public void Dispose() {
            ListenForMessages( false );
        }

        private void ListenForMessages( bool i_listen ) {
            if ( i_listen ) {
                MyMessenger.Instance.AddListener<IClientMoveAttempt>( ClientGameEvents.MADE_MOVE, OnMadeMove );
                MyMessenger.Instance.AddListener<IGameObstaclesUpdate>( ClientMessages.UPDATE_OBSTACLES, OnUpdateObstacles );
            }
            else {
                MyMessenger.Instance.RemoveListener<IClientMoveAttempt>( ClientGameEvents.MADE_MOVE, OnMadeMove );
                MyMessenger.Instance.RemoveListener<IGameObstaclesUpdate>( ClientMessages.UPDATE_OBSTACLES, OnUpdateObstacles );
            }
        }

        public void OnMadeMove( IClientMoveAttempt i_attempt ) {
            SetInteractableProperties( true );
        }

        public void ResetMoves() {
            MyMessenger.Instance.Send( ClientGameEvents.RESET_MOVES );
            MyMessenger.Instance.Send( ClientMessages.UPDATE_OBSTACLES, CachedUpdate );
            SetInteractableProperties( false );
        }

        public void OnUpdateObstacles( IGameObstaclesUpdate i_update ) {
            CachedUpdate = i_update;
        }

        private void SetInteractableProperties( bool i_interactable ) {
            ViewModel.SetProperty( VISIBLE_PROPERTY, i_interactable ? 1f : 0f );
            ViewModel.SetProperty( USE_PROPERTY, i_interactable );
        }
    }
}