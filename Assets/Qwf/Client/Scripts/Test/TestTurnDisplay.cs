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
                SendUpdate( "Me" );
            } else if ( Input.GetKeyDown( KeyCode.X ) ) {
                SendUpdate( "Them" );
            }
        }

        private void SendUpdate( string i_player ) {
            ITurnUpdate update = new TurnUpdate() { ActivePlayer = i_player };
            MyMessenger.Instance.Send( ClientMessages.UPDATE_TURN, update );
        }
    }
}
