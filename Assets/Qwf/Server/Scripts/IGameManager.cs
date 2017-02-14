
namespace Qwf {
    public interface IGameManager {
        void TryPlayerTurn( IPlayerTurn i_turn );
        void SetGameBoard( IGameBoard i_board );
        void SetScoreKeeper( IScoreKeeper i_scoreKeeper );
        void AddPlayer( IGamePlayer i_player );

        bool IsReady();
        bool IsPlayerTurnValidForGameState( IPlayerTurn i_turn );
        bool IsGameOver();    

        IGamePlayer GetPlayerFromId( string i_id );
        IGamePlayer ActivePlayer { get; }
        IGamePlayer InactivePlayer { get; }

        IGameBoard Board { get; }
    }
}