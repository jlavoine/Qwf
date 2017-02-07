using UnityEngine;

namespace Qwf.Client {
    public class TestGameWithManager : MonoBehaviour {
        private ClientGameManager mManager;

        void Awake() {
            mManager = new ClientGameManager();
        }

        void OnDestroy() {
            mManager.Dispose();
        }
    }
}