using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyLibrary;

namespace Qwf.Client {
    public class TestPlayerHand : MonoBehaviour {
        public PlayerHandView View;

        void Start() {
        }

        public List<GamePieceData> CreateRandom( int i_num ) {
            List<GamePieceData> pieces = new List<GamePieceData>();

            for ( int i = 0; i < i_num; ++i ) {
                pieces.Add( new GamePieceData() { Value = Random.Range( 1, 5 ), PieceType = Random.Range( 1, 6 ), Owner = "Me" } );
            }

            return pieces;

        }

        void Update() {
            if ( Input.GetKeyDown( KeyCode.Space ) ) {
                List<GamePieceData> pieces = CreateRandom(Random.Range(4,6));
                //OnUpdatePlayerHand
             
                PlayerHandUpdateData data = new PlayerHandUpdateData();
                data.GamePieces = pieces;
                data.Id = "Me";
                MyMessenger.Instance.Send<PlayerHandUpdateData>( ClientMessages.UPDATE_HAND, data );
            }
        }

    }
}