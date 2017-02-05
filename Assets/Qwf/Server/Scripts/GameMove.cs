
namespace Qwf {
    public class GameMove : IGameMove {
        private IServerGamePiece mTargetPiece;
        private IGameObstacle mTargetObstacle;
        private IGamePieceSlot mTargetSlot;

        public GameMove( IServerGamePiece i_targetPiece, IGameObstacle i_targetObstacle, IGamePieceSlot i_targetSlot ) {
            mTargetPiece = i_targetPiece;
            mTargetObstacle = i_targetObstacle;
            mTargetSlot = i_targetSlot;
        }

        public IServerGamePiece GetTargetPiece() {
            return mTargetPiece;
        }

        public IGameObstacle GetTargetObstacle() {
            return mTargetObstacle;
        }

        public IGamePieceSlot GetTargetSlot() {
            return mTargetSlot;
        }

        public bool IsLegal( IGameBoard i_board ) {
            bool doesPlayerCurrentlyHoldPiece = mTargetPiece.IsCurrentlyHeld();
            bool isObstacleCurrent = i_board.IsObstacleCurrent( mTargetObstacle );
            bool canPieceBePlacedInObstacleSlot = mTargetObstacle.CanPieceBePlacedIntoSlot( mTargetPiece, mTargetSlot );

            return doesPlayerCurrentlyHoldPiece && isObstacleCurrent && canPieceBePlacedInObstacleSlot;
        }

        public void MakeMove() {
            mTargetPiece.PlaceFromPlayerHandIntoSlot( mTargetSlot );
        }
    }
}