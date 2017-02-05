using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyLibrary;

namespace Qwf {
    public class InitOfflineServices : MonoBehaviour {

        void Awake() {
            BackendManager.Instance.Init( new OfflineBackend() );
        }
    }
}
