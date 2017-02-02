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
    }
}
