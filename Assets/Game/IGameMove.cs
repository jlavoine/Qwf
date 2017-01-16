
namespace Qwf {
    public interface IGameMove {
        IGamePiece GetTargetPiece();
        IGameObstacle GetTargetObstacle();
        IGamePieceSlot GetTargetSlot();
    }
}