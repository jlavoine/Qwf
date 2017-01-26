﻿using System.Collections.Generic;

namespace Qwf {
    public class GameManager : IGameManager {
        private IGameBoard mBoard;

        public GameManager( IGameBoard i_board ) {
            mBoard = i_board;
        }

        public void TryPlayerTurn( IPlayerTurn i_turn ) {
            if ( i_turn.IsValid( mBoard ) ) {
                ProcessTurn( i_turn );
                FillPlayerHandAfterTurn( i_turn.GetPlayer() );
                UpdateBoardStateAfterTurn();
            }
        }

        private void ProcessTurn( IPlayerTurn i_turn ) {
            i_turn.Process();
        }

        private void FillPlayerHandAfterTurn( IGamePlayer i_player ) {
            i_player.DrawToFillHand();
        }

        private void UpdateBoardStateAfterTurn() {
            mBoard.UpdateBoardState();
        }
    }
}