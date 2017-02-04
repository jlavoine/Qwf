using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Qwf;

public class MyJsonTester : MonoBehaviour {

	// Use this for initialization
	void Start () {
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

        TestInterfaces();
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
}
