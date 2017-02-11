
namespace Qwf {
    public interface IGameManager {
        void TryPlayerTurn( IPlayerTurn i_turn );
        void SetGameBoard( IGameBoard i_board );
        void SetScoreKeeper( IScoreKeeper i_scoreKeeper );
        void AddPlayer( IGamePlayer i_player, string i_id );

        bool IsReady();
        bool IsPlayerTurnValidForGameState( IPlayerTurn i_turn );      

        IGamePlayer GetPlayerFromId( string i_id );

        IGameBoard Board { get; }
    }
}