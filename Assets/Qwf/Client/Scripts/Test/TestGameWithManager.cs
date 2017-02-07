using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qwf.Client {
    public class TestGameWithManager : MonoBehaviour {
        private ClientGameManager mManager;

        // Use this for initialization
        void Awake() {
            mManager = new ClientGameManager();
        }

        void OnDestroy() {
            mManager.Dispose();
        }

        // Update is called once per frame
        void Update() {

        }
    }
}