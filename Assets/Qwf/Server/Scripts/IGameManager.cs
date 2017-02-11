
namespace Qwf {
    public interface IGameManager {
        void TryPlayerTurn( IPlayerTurn i_turn );
        void SetGameBoard( IGameBoard i_board );
        void SetScoreKeeper( IScoreKeeper i_scoreKeeper );
        void AddPlayer( IGamePlayer i_player, string i_id );

        bool IsReady();

        IGamePlayer GetPlayerFromId( string i_id );

        IGameBoard Board { get; }
    }
}