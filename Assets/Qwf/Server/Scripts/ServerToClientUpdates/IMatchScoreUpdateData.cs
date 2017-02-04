
namespace Qwf {
    public interface IMatchScoreUpdateData {
        int GetScoreForPlayer( string i_playerId );
        int GetScoreForOpponent( string i_playerId );
    }
}
