using MyLibrary;

namespace Qwf {
    public class TurnUpdate : ITurnUpdate {
        public string ActivePlayer;

        public string GetActivePlayer() {
            return ActivePlayer;
        }

        public bool IsThisPlayerActive() {
            string thisPlayer = BackendManager.Instance.GetPlayerId();
            string activePlayer = GetActivePlayer();

            return thisPlayer == activePlayer;
        }
    }
}
