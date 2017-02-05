
namespace Qwf {
    public interface IGameMove {
        IServerGamePiece GetTargetPiece();
        IGameObstacle GetTargetObstacle();
        IGamePieceSlot GetTargetSlot();

        bool IsLegal( IGameBoard i_board );

        void MakeMove();
    }
}