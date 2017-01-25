
namespace Qwf {
    public interface IPlayerTurn {
        bool IsValid();
        void Process();

        IGamePlayer GetPlayer();
    }
}