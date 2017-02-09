﻿
namespace Qwf {
    public class GameManager : IGameManager {
        private IGameBoard mBoard;
        public IGameBoard GameBoard { get { return mBoard; } }

        private IScoreKeeper mScoreKeeper;

        public GameManager( IGameBoard i_board, IScoreKeeper i_scoreKeeper ) {
            mBoard = i_board;
            mScoreKeeper = i_scoreKeeper;
        }

        public void TryPlayerTurn( IPlayerTurn i_turn ) {
            if ( i_turn.IsValid( mBoard ) ) {
                ProcessTurn( i_turn );
                FillPlayerHandAfterTurn( i_turn.GetPlayer() );
                UpdateBoardStateAfterTurn( i_turn.GetPlayer() );
                CheckForGameOver();
            }
        }

        private void ProcessTurn( IPlayerTurn i_turn ) {
            i_turn.Process();
        }

        private void FillPlayerHandAfterTurn( IGamePlayer i_player ) {
            i_player.DrawToFillHand();
        }

        private void UpdateBoardStateAfterTurn( IGamePlayer i_currentPlayer ) {
            mBoard.UpdateBoardState( mScoreKeeper, i_currentPlayer );
        }

        private void CheckForGameOver() {
            if ( mBoard.IsGameOver() ) {
            }
        }
    }
}