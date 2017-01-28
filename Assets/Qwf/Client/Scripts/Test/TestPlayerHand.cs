using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Qwf.Client {
    public class TestPlayerHand : MonoBehaviour {
        public PlayerHandView View;

        void Start() {
            IGamePlayer player = new ClientPlayer( 0 );

            List<IGamePiece> pieces = new List<IGamePiece>();
            pieces.Add( new GamePiece( player, new GamePieceData() { Value = 1, PieceType = 0 } ) );
            pieces.Add( new GamePiece( player, new GamePieceData() { Value = 2, PieceType = 1 } ) );
            pieces.Add( new GamePiece( player, new GamePieceData() { Value = 3, PieceType = 2 } ) );
            pieces.Add( new GamePiece( player, new GamePieceData() { Value = 4, PieceType = 3 } ) );
            pieces.Add( new GamePiece( player, new GamePieceData() { Value = 5, PieceType = 4 } ) );

            View.Init( new PlayerHandPM( pieces, 0 ) );
        }

    }
}