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

        public void AddPointsToPlayer( IGamePlayer i_player, int i_points ) {
            AddPointsToPlayer( i_player.Id, i_points );
        }

        public void AddPointsToPlayer( string i_player, int i_points ) {
            int currentScore = mPlayerScores[i_player];
            mPlayerScores[i_player] = currentScore + i_points;
        }
    }
}
