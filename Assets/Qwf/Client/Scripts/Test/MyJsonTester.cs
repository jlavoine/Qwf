using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Qwf {
    public class MyJsonTester : MonoBehaviour {

        // Use this for initialization
        void Start() {
            /*PlayerDeckData test = new PlayerDeckData();
            test.GamePieces = new List<DeckGamePieceData>();
            DeckGamePieceData one = new DeckGamePieceData();
            one.PieceType = 0;
            one.PieceValueToCount = new Dictionary<int, int>() { { 0, 1 }, { 3, 2 } };

            DeckGamePieceData two = new DeckGamePieceData();
            two.PieceType = 1;
            two.PieceValueToCount = new Dictionary<int, int>() { { 0, 1 }, { 3, 2 } };

            test.GamePieces.Add( one );
            test.GamePieces.Add( two );

            string json = JsonConvert.SerializeObject( test );
            UnityEngine.Debug.LogError( json );
            */

            //TestInterfaces();

            //TestObstacleData();

            TestInterfaces2();
        }

        private void TestObstacleData() {
            List<GameObstacleData> list = new List<GameObstacleData>();
            GameObstacleData one = new GameObstacleData();
            one.FinalBlowValue = 3;
            one.SlotsData = new List<GamePieceSlotData>();

            GamePieceSlotData slotOne = new GamePieceSlotData();
            slotOne.PieceType = 1;
            slotOne.ScoreValue = 1;

            GamePieceSlotData slot2 = new GamePieceSlotData();
            slot2.PieceType = 3;
            slot2.ScoreValue = 1;

            one.SlotsData.Add( slotOne );
            one.SlotsData.Add( slot2 );

            GameObstacleData two = new GameObstacleData();
            two.FinalBlowValue = 2;
            two.SlotsData = new List<GamePieceSlotData>();
            two.SlotsData.Add( slotOne );

            list.Add( one );
            list.Add( two );

            string json = JsonConvert.SerializeObject( list );
            UnityEngine.Debug.LogError( json );
        }

        private void TestInterfaces() {
            GameObstaclesUpdate obstacles = new GameObstaclesUpdate();
            obstacles.Obstacles = new List<GameObstacleUpdate>();

            GameObstacleUpdate one = new GameObstacleUpdate();
            one.Id = "Goblin";

            GameObstacleUpdate two = new GameObstacleUpdate();
            two.Id = "Blob";

            obstacles.Obstacles.Add( one );
            obstacles.Obstacles.Add( two );

            string json = JsonConvert.SerializeObject( obstacles );
            UnityEngine.Debug.LogError( json );

            GameObstaclesUpdate deserial = JsonConvert.DeserializeObject<GameObstaclesUpdate>( json );
            IGameObstacleUpdate update1 = deserial.GetUpdate( 0 );
            UnityEngine.Debug.LogError( update1.GetId() );

            IGameObstacleUpdate update2 = deserial.GetUpdate( 1 );
            UnityEngine.Debug.LogError( update2.GetId() );
        }

        private void TestInterfaces2() {

        }
    }
}