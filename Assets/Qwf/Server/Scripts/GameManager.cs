using System.Collections.Generic;

namespace Qwf {
    public class GameManager : IGameManager {
        private IGameBoard mBoard;
        public IGameBoard Board { get { return mBoard; } }

        private IScoreKeeper mScoreKeeper;

        private Dictionary<string, IGamePlayer> mPlayers = new Dictionary<string, IGamePlayer>();

        public GameManager() { }
        public GameManager( IGameBoard i_board, IScoreKeeper i_scoreKeeper ) {
            SetGameBoard( i_board );
            SetScoreKeeper( i_scoreKeeper );
        }

        public bool IsReady() {
            return mBoard != null && mScoreKeeper != null;
        }

        public void AddPlayer( IGamePlayer i_player, string i_id ) {
            mPlayers.Add( i_id, i_player );
        }

        public IGamePlayer GetPlayerFromId( string i_id ) {
            if ( mPlayers.ContainsKey( i_id ) ) {
                return mPlayers[i_id];
            } else {
                return null;
            }
        }

        public void SetGameBoard( IGameBoard i_board ) {
            mBoard = i_board;
        }

        public void SetScoreKeeper( IScoreKeeper i_scoreKeeper ) {
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