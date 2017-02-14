using MyLibrary;

namespace Qwf {
    public class GameOverUpdate : IGameOverUpdate {
        public string Winner;

        public bool DidClientWin() {
            return BackendManager.Instance.GetPlayerId() == Winner;
        }
    }
}
