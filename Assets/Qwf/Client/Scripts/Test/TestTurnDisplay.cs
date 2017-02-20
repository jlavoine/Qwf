using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyLibrary;

namespace Qwf.Client {
    public class TestTurnDisplay : MonoBehaviour {

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            if ( Input.GetKeyDown( KeyCode.Z ) ) {
                SendUpdate( true );
            } else if ( Input.GetKeyDown( KeyCode.X ) ) {
                SendUpdate( false );
            }
        }

        private void SendUpdate( bool i_isActivePlayer ) {
            ITurnUpdate update = new TurnUpdate() { IsPlayerActive = i_isActivePlayer };
            MyMessenger.Instance.Send( ClientMessages.UPDATE_TURN, update );
        }
    }
}
