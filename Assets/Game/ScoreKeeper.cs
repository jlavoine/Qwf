using System.Collections.Generic;

namespace Qwf {
    public class ScoreKeeper : IScoreKeeper {
        private Dictionary<IGamePlayer, int> mPlayerScores = new Dictionary<IGamePlayer, int>();

        public ScoreKeeper() { }

        public void AddPlayer( IGamePlayer i_player ) {
            int noScore = 0;
            mPlayerScores.Add( i_player, noScore );
        }

        public int GetPlayerScore( IGamePlayer i_player ) {
            return mPlayerScores[i_player];
        }

        public void AddPointsToPlayer( IGamePlayer i_player, int i_points ) {
            int currentScore = mPlayerScores[i_player];
            mPlayerScores[i_player] = currentScore + i_points;
        }
    }
}
