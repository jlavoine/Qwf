using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using MyLibrary;

namespace Qwf.Client {
    public class TestMatchScoreView : MonoBehaviour {
        public MatchScoreView ScoreView;

        // Use this for initialization
        void Start() {
            /*MatchScoreUpdateData test = new MatchScoreUpdateData();
            test.Scores = new Dictionary<string, int>();
            test.Scores.Add( "Hi", 1001 );

            string json = JsonConvert.SerializeObject( test );
            UnityEngine.Debug.LogError( test );

            IMatchScoreUpdateData test2 = JsonConvert.DeserializeObject<MatchScoreUpdateData>( json );
            UnityEngine.Debug.LogError( test2.GetScoreForPlayer( "Hi" ) );*/

            MatchScorePM pm = new MatchScorePM( "Me" );

            ScoreView.Init( pm );
        }

        // Update is called once per frame
        void Update() {
            if (Input.GetKeyDown(KeyCode.S)) {
                SendFakeScore();
            }
        }

        private void SendFakeScore() {
            MatchScoreUpdateData update = new MatchScoreUpdateData();
            update.Scores = new Dictionary<string, int>();
            update.Scores.Add( "Me", Random.Range(1,100) );
            update.Scores.Add( "Them", Random.Range( 1, 100 ) );

            MyMessenger.Instance.Send<IMatchScoreUpdateData>( ClientMessages.UPDATE_SCORE, update );   
        }
    }
}