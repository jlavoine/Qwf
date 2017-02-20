using UnityEngine;

namespace Qwf.Client {
    public class ClientRelayGameObject : MonoBehaviour {
        ClientRelay mRelay;

        void Awake() {
            mRelay = new ClientRelay();
        }

        void OnDestroy() {
            mRelay.Dispose();
        }
    }
}
