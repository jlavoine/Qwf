using System.Collections.Generic;

namespace Qwf {
    public class MatchScoreUpdateData : IMatchScoreUpdateData {
        public Dictionary<string, int> Scores;

        public int GetScoreForPlayer( string i_playerId ) {
            if ( Scores.ContainsKey( i_playerId ) ) {
                return Scores[i_playerId];
            } else {
                return 0;   // fallback if the player id wasn't in the scores
            }
        }

        public int GetScoreForOpponent( string i_playerId ) {
            foreach ( KeyValuePair<string, int> kvp in Scores ) {
                if ( kvp.Key != i_playerId ) {
                    return kvp.Value;
                }
            }

            return 0;   // fallback if no opponent was found
        }

        public static MatchScoreUpdateData Create( IGamePlayer i_player1, IGamePlayer i_player2, IScoreKeeper i_scoreKeeper ) {
            MatchScoreUpdateData data = new MatchScoreUpdateData();
            data.Scores = new Dictionary<string, int>();
            data.Scores.Add( i_player1.Id, i_scoreKeeper.GetPlayerScore( i_player1 ) );
            data.Scores.Add( i_player2.Id, i_scoreKeeper.GetPlayerScore( i_player2 ) );

            return data;
        }
    }
}
