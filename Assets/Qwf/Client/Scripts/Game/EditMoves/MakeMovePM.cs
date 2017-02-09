using MyLibrary;

namespace Qwf.Client {
    public abstract class MakeMovePM : GenericViewModel {

        public virtual void ProcessAction() {
            ChangeTurnFromActivePlayer();
        }

        protected void ChangeTurnFromActivePlayer() {
            TurnUpdate update = new TurnUpdate();
            update.ActivePlayer = string.Empty; // anyone but this player means it's not this player's turn

            MyMessenger.Instance.Send<ITurnUpdate>( ClientMessages.UPDATE_TURN, update );
        }        
    }
}
