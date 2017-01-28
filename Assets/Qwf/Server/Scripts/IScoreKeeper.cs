
namespace Qwf {
    public interface IScoreKeeper {
        int GetPlayerScore( IGamePlayer i_player );

        void AddPlayer( IGamePlayer i_player );
        void AddPointsToPlayer( IGamePlayer i_player, int i_points );
    }
}