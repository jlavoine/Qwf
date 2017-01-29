using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Qwf;

public class MyJsonTester : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerDeckData test = new PlayerDeckData();
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
    }
}
