
namespace Qwf.Client {
    public interface IPlayerHandGamePiecePM : IGamePiecePM {
        void Play();
        void InvalidPlayAttempt();
    }
}