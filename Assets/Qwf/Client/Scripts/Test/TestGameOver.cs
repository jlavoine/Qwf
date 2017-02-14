using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyLibrary;

namespace Qwf {
    public class TestGameOver : MonoBehaviour {

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            if ( Input.GetKeyDown( KeyCode.W ) ) {
                GameOverUpdate update = new GameOverUpdate();
                update.Winner = "Me";
                MyMessenger.Instance.Send<IGameOverUpdate>( ClientMessages.GAME_OVER_UPDATE, update );
            } else if ( Input.GetKeyDown( KeyCode.L ) ) {
                GameOverUpdate update = new GameOverUpdate();
                update.Winner = "Them";
                MyMessenger.Instance.Send<IGameOverUpdate>( ClientMessages.GAME_OVER_UPDATE, update );
            }
        }
    }
}
