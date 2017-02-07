using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyLibrary;

namespace Qwf.Client {
    public class TestObstaclesView : MonoBehaviour {
        public GameObstaclesView View;
        private GameObstaclesPM mPM;

        // Use this for initialization
        void Start() {

        }

        private IGameObstaclesUpdate CreateRandomUpdate( int i_numObstacles ) {
            GameObstaclesUpdate update = new GameObstaclesUpdate();
            update.Obstacles = new List<GameObstacleUpdate>();

            for ( int i = 0; i < i_numObstacles; ++i ) {
                update.Obstacles.Add( CreateRandomObstacleUpdate() );
            }

            return update;
        }

        private GameObstacleUpdate CreateRandomObstacleUpdate() {
            GameObstacleUpdate update = new GameObstacleUpdate();
            update.Id = GetRandomObstacleId();
            update.FinalBlowValue = Random.Range( 1, 6 );
            update.PieceSlots = GetRandomPieceSlots( Random.Range(3,6) );         

            return update;
        }

        private List<GamePieceSlotUpdate> GetRandomPieceSlots( int i_numSlots ) {
            List<GamePieceSlotUpdate> updates = new List<GamePieceSlotUpdate>();
            for ( int i = 0; i < i_numSlots; ++i ) {
                GamePieceSlotUpdate update = new GamePieceSlotUpdate();
                update.SlotPieceType = Random.Range( 1, 6 );
                update.PieceInSlot = GetRandomPieceInSlot( update.SlotPieceType );
                update.ScoreValue = 1;
                updates.Add( update );
            }

            return updates;
        }

        private GamePieceData GetRandomPieceInSlot( int i_slotType ) {
            int random = Random.Range( 0, 3 );
            switch ( random ) {
                case 0:
                    // null; no piece in slot
                    return null;
                case 1:
                    // the player's own piece
                    GamePieceData myPiece = new GamePieceData();
                    myPiece.Owner = "Me";
                    myPiece.Value = Random.Range( 1, 6 );
                    myPiece.PieceType = i_slotType;
                    return myPiece;
                default:
                    // opponent piece
                    GamePieceData theirPiece = new GamePieceData();
                    theirPiece.Owner = "Them";
                    theirPiece.Value = Random.Range( 1, 6 );
                    theirPiece.PieceType = i_slotType;
                    return theirPiece;
            }
        }

        private string GetRandomObstacleId() {
            List<string> ids = new List<string>() { "Dragon", "Blob", "WizGob", "Goblin", "Chest" };
            return ListUtils.GetRandomElement<string>( ids );
        }

        // Update is called once per frame
        void Update() {
            if ( Input.GetKeyDown( KeyCode.M ) ) {
                SendRandomUpdate();
            }
        }

        private void SendRandomUpdate() {
            if ( mPM == null ) {
                IGameObstaclesUpdate randomUpdate = CreateRandomUpdate( 3 );
                mPM = new GameObstaclesPM( randomUpdate );
                MyMessenger.Instance.Send<IGameObstaclesUpdate>( ClientMessages.UPDATE_OBSTACLES, randomUpdate );

                View.Init( mPM );
            }
            else {
                IGameObstaclesUpdate randomUpdate = CreateRandomUpdate( Random.Range( 1, 4 ) );
                MyMessenger.Instance.Send<IGameObstaclesUpdate>( ClientMessages.UPDATE_OBSTACLES, randomUpdate );
            }
        }
    }
}