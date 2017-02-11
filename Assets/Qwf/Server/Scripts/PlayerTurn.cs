using System.Collections.Generic;

namespace Qwf {
    public class PlayerTurn : IPlayerTurn {
        private IGamePlayer mPlayer;
        private List<IGameMove> mMoves;

        public const int MAX_MOVES_IN_TURN = 3; // TODO constants?

        public PlayerTurn( IGamePlayer i_player, List<IGameMove> i_moves ) {
            mPlayer = i_player;
            mMoves = i_moves;
        }

        public IGamePlayer GetPlayer() {
            return mPlayer;
        }

        public bool IsValid( IGameBoard i_board ) {
            return mMoves.Count <= MAX_MOVES_IN_TURN && AreMovesLegal( i_board ) && !AreAnyDuplicatePiecesInMoves();
        }

        public void Process() {
            foreach ( IGameMove move in mMoves ) {
                move.MakeMove();
            }
        }

        private bool AreMovesLegal( IGameBoard i_board ) {
            foreach ( IGameMove move in mMoves ) {
                if ( !move.IsLegal( i_board ) ) {
                    return false;
                }
            }

            return true;
        }

        private bool AreAnyDuplicatePiecesInMoves() {
            foreach ( IGameMove move in mMoves ) {
                int count = 0;
                IServerGamePiece targetPiece = move.GetTargetPiece();
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