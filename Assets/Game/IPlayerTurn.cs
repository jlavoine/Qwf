
namespace Qwf {
    public interface IPlayerTurn {
        bool IsValid( IGameBoard i_board );
        void Process();

        IGamePlayer GetPlayer();
    }
}