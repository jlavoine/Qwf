using System.Collections.Generic;

namespace Qwf {
    public class GameManager : IGameManager {
        private IGameBoard mBoard;

        public GameManager( IGameBoard i_board ) {
            mBoard = i_board;
        }

        public void AttemptMoves( List<IGameMove> i_moves ) {
            if ( AreMovesLegal( i_moves ) ) {
                MakeMoves( i_moves );
            }
        }

        public bool AreMovesLegal( List<IGameMove> i_moves ) {
            foreach (IGameMove move in i_moves ) {
                if ( !move.IsLegal( mBoard ) ) {
                    return false;
                }
            }

            return true;
        }       

        private void MakeMoves( List<IGameMove> i_moves ) {
            foreach ( IGameMove move in i_moves ) {
                move.MakeMove();
            }
        }
    }
}