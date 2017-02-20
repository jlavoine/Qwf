
namespace Qwf.Client {
    public interface IPlayerManager {
        IPlayerData Data { get; }

        void Init( IPlayerData i_data );
    }
}
