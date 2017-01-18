
namespace Qwf {
    public interface IGameMove {
        IGamePiece GetTargetPiece();
        IGameObstacle GetTargetObstacle();
        IGamePieceSlot GetTargetSlot();

        bool IsLegal( IGameBoard i_board );

        void MakeMove();
    }
}