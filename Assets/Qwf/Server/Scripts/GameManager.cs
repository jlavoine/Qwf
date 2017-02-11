using System.Collections.Generic;

namespace Qwf {
    public class GameManager : IGameManager {
        private IGameBoard mBoard;
        public IGameBoard Board { get { return mBoard; } }
        
        // if there are more than 2-player games, will need to re-archtitect this (obviously)
        private IGamePlayer mActivePlayer;
        private IGamePlayer mInactivePlayer;
        public IGamePlayer ActivePlayer { get { return mActivePlayer; } set { mActivePlayer = value; } }
        public IGamePlayer InactivePlayer { get { return mInactivePlayer; } set { mInactivePlayer = value; } }

        private IScoreKeeper mScoreKeeper;

        private Dictionary<string, IGamePlayer> mPlayers = new Dictionary<string, IGamePlayer>();

        public GameManager() { }
        public GameManager( IGameBoard i_board, IScoreKeeper i_scoreKeeper ) {
            SetGameBoard( i_board );
            SetScoreKeeper( i_scoreKeeper );
        }

        public bool IsReady() {
            return mBoard != null && mScoreKeeper != null && mPlayers.Count == 2;
        }

        public void AddPlayer( IGamePlayer i_player ) {
            mPlayers.Add( i_player.Id, i_player );
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
            PickActivePlayerIfReady();
        }

        public void SetScoreKeeper( IScoreKeeper i_scoreKeeper ) {
            mScoreKeeper = i_scoreKeeper;
            PickActivePlayerIfReady();
        }

        public void TryPlayerTurn( IPlayerTurn i_turn ) {
            if ( IsPlayerTurnValidForGameState( i_turn ) ) {
                ProcessTurn( i_turn );
                SwitchActivePlayer();
                FillPlayerHandAfterTurn( i_turn.GetPlayer() );
                UpdateBoardStateAfterTurn( i_turn.GetPlayer() );
                CheckForGameOver();
            }
        }

        public bool IsPlayerTurnValidForGameState( IPlayerTurn i_turn ) {
            return i_turn.IsValid( mBoard );
        }

        private void PickActivePlayerIfReady() {
            if ( IsReady() ) {
                int i = 0;
                foreach ( KeyValuePair<string, IGamePlayer> kvp in mPlayers ) {
                    if ( i == 0 ) {
                        mActivePlayer = kvp.Value;
                        i++;
                    } else {
                        mInactivePlayer = kvp.Value;
                    }
                }
            }
        }

        private void SwitchActivePlayer() {
            IGamePlayer playerWhoJustTookTurn = mActivePlayer;
            mActivePlayer = mInactivePlayer;
            mInactivePlayer = playerWhoJustTookTurn;
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