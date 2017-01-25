using System.Collections.Generic;

namespace Qwf {
    public class PlayerTurn : IPlayerTurn {
        private IGamePlayer mPlayer;
        private List<IGameMove> mMoves;
        private IGameBoard mBoard;

        public PlayerTurn( IGamePlayer i_player, List<IGameMove> i_moves, IGameBoard i_board ) {
            mPlayer = i_player;
            mMoves = i_moves;
            mBoard = i_board;
        }

        public IGamePlayer GetPlayer() {
            return mPlayer;
        }

        public bool IsValid() {
            return AreMovesLegal() && !AreAnyDuplicatePiecesInMoves();
        }

        public void Process() {
            foreach ( IGameMove move in mMoves ) {
                move.MakeMove();
            }
        }

        private bool AreMovesLegal() {
            foreach ( IGameMove move in mMoves ) {
                if ( !move.IsLegal( mBoard ) ) {
                    return false;
                }
            }

            return true;
        }

        private bool AreAnyDuplicatePiecesInMoves() {
            foreach ( IGameMove move in mMoves ) {
                int count = 0;
                IGamePiece targetPiece = move.GetTargetPiece();
                foreach ( IGameMove otherMove in mMoves ) {
                    if ( otherMove.GetTargetPiece() == targetPiece ) {
                        count++;
                    }
                }

                if ( count > 1 ) {
                    return true;
                }
            }

            return false;
        }
    }
}