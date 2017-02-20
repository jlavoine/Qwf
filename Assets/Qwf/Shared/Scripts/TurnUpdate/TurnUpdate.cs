using MyLibrary;

namespace Qwf {
    public class TurnUpdate : ITurnUpdate {
        // delaying this until a nickname system...I don't want to be sending other player's IDs around
        /*public string ActivePlayer;

        public string GetActivePlayer() {
            return ActivePlayer;
        }*/

        public bool IsPlayerActive;

        public bool IsThisPlayerActive() {
            return IsPlayerActive;
            /*string thisPlayer = BackendManager.Instance.GetPlayerId();
            string activePlayer = GetActivePlayer();

            return thisPlayer == activePlayer;*/
        }
    }
}
