using MyLibrary;
using System.Collections.Generic;

namespace Qwf.Client {
    public class MatchScorePM : GenericViewModel {
        public const string PLAYER_SCORE_PROPERTY = "PlayerScore";
        public const string OPPONENT_SCORE_PROPERTY = "OpponentScore";

        private string mPlayerId;

        public MatchScorePM( string i_playerId ) {
            mPlayerId = i_playerId;

            SetPlayerScoreProperty( 0 );
            SetOpponentScoreProperty( 0 );

            ListenForMessages( true );
        }

        public void Dispose() {
            ListenForMessages( false );
        }

        private void ListenForMessages( bool i_listen ) {
            if ( i_listen ) {
                MyMessenger.Instance.AddListener<IMatchScoreUpdateData>( ClientMessages.UPDATE_SCORE, OnUpdateScore ); 
            } else {
                MyMessenger.Instance.RemoveListener<IMatchScoreUpdateData>( ClientMessages.UPDATE_SCORE, OnUpdateScore );
            }
        }

        public void OnUpdateScore( IMatchScoreUpdateData i_update ) {
            SetPlayerScoreProperty( i_update.GetScoreForPlayer( mPlayerId ) );
            SetOpponentScoreProperty( i_update.GetScoreForOpponent( mPlayerId ) );
        }

        private void SetPlayerScoreProperty( int i_score ) {
            ViewModel.SetProperty( PLAYER_SCORE_PROPERTY, i_score.ToString() );
        }

        private void SetOpponentScoreProperty( int i_score ) {
            ViewModel.SetProperty( OPPONENT_SCORE_PROPERTY, i_score.ToString() );
        }
    }
}
