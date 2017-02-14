using System.Collections.Generic;

namespace Qwf {
    public class ScoreKeeper : IScoreKeeper {
        private Dictionary<string, int> mPlayerScores = new Dictionary<string, int>();

        public ScoreKeeper() { }

        public void AddPlayer( IGamePlayer i_player ) {            
            int noScore = 0;
            mPlayerScores.Add( i_player.Id, noScore );
        }

        public int GetPlayerScore( IGamePlayer i_player ) {
            return mPlayerScores[i_player.Id];
        }

        public int GetNumPlayers() {
            return mPlayerScores.Count;
        }

        public void AddPointsToPlayer( IGamePlayer i_player, int i_points ) {
            AddPointsToPlayer( i_player.Id, i_points );
        }

        public void AddPointsToPlayer( string i_player, int i_points ) {
            int currentScore = mPlayerScores[i_player];
            mPlayerScores[i_player] = currentScore + i_points;
        }

        public string GetWinner() {
            int scoreToBeat = 0;
            string winner = string.Empty;
            foreach ( KeyValuePair<string, int> kvp in mPlayerScores ) {
                if ( kvp.Value > scoreToBeat ) {
                    scoreToBeat = kvp.Value;
                    winner = kvp.Key;
                }
            }

            return winner;
        }
    }
}
