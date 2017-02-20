using MyLibrary;

namespace Qwf.Client {
    public abstract class MakeMovePM : GenericViewModel {
        public const string VISIBLE_PROPERTY = "IsVisible";
        public const string USE_PROPERTY = "CanUse";

        public virtual void ProcessAction() {
            ChangeTurnFromActivePlayer();
        }

        protected void ChangeTurnFromActivePlayer() {
            TurnUpdate update = new TurnUpdate();
            update.IsPlayerActive = false; // anyone but this player means it's not this player's turn

            MyMessenger.Instance.Send<ITurnUpdate>( ClientMessages.UPDATE_TURN, update );
        }

        protected void SetInteractableProperties( bool i_interactable ) {
            ViewModel.SetProperty( VISIBLE_PROPERTY, i_interactable ? 1f : 0f );
            ViewModel.SetProperty( USE_PROPERTY, i_interactable );
        }
    }
}
