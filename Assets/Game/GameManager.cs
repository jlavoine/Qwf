using System.Collections.Generic;

namespace Qwf {
    public class GameManager : IGameManager {
        private IGameBoard mBoard;

        public GameManager( IGameBoard i_board ) {
            mBoard = i_board;
        }

        public void AttemptMoves( IGamePlayer i_player, List<IGameMove> i_moves ) {
            if ( !AreAnyDuplicatePiecesInMoves( i_moves ) && AreMovesLegal( i_moves ) ) {
                MakeMoves( i_moves );
                i_player.DrawToFillHand();
            }
        }

        private bool AreMovesLegal( List<IGameMove> i_moves ) {
            foreach (IGameMove move in i_moves ) {
                if ( !move.IsLegal( mBoard ) ) {
                    return false;
                }
            }

            return true;
        }

        private bool AreAnyDuplicatePiecesInMoves( List<IGameMove> i_moves ) {
            foreach ( IGameMove move in i_moves ) {
                int count = 0;
                IGamePiece targetPiece = move.GetTargetPiece();
                foreach ( IGameMove otherMove in i_moves ) {
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

        private void MakeMoves( List<IGameMove> i_moves ) {
            foreach ( IGameMove move in i_moves ) {
                move.MakeMove();
            }
        }
    }
}