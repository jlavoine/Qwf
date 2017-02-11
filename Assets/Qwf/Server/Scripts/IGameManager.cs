
namespace Qwf {
    public interface IGameManager {
        void TryPlayerTurn( IPlayerTurn i_turn );
        void SetGameBoard( IGameBoard i_board );
        void SetScoreKeeper( IScoreKeeper i_scoreKeeper );

        bool IsReady();

        IGameBoard Board { get; }
    }
}