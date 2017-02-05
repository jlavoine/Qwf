using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyLibrary;

namespace Qwf.Client {
    public class TestObstaclesView : MonoBehaviour {
        public GameObstaclesView View;

        // Use this for initialization
        void Start() {
            IGameObstaclesUpdate randomUpdate = CreateRandomUpdate( 3 );
            GameObstaclesPM pm = new GameObstaclesPM( randomUpdate );

            View.Init( pm );
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
            update.PieceSlots = GetRandomPieceSlots( 5 );

            return update;
        }

        private List<GamePieceSlotUpdate> GetRandomPieceSlots( int i_numSlots ) {
            List<GamePieceSlotUpdate> updates = new List<GamePieceSlotUpdate>();
            for ( int i = 0; i < i_numSlots; ++i ) {
                GamePieceSlotUpdate update = new GamePieceSlotUpdate();
                update.SlotPieceType = Random.Range( 0, 5 );
                updates.Add( update );
            }

            return updates;
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
            IGameObstaclesUpdate randomUpdate = CreateRandomUpdate( Random.Range(1,4) );
            MyMessenger.Instance.Send<IGameObstaclesUpdate>( ClientMessages.UPDATE_OBSTACLES, randomUpdate );
        }
    }
}