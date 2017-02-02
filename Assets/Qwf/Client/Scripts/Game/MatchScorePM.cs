using MyLibrary;
using System.Collections.Generic;

namespace Qwf.Client {
    public class MatchScorePM : GenericViewModel {
        public const string PLAYER_SCORE_PROPERTY = "PlayerScore";
        public const string OPPONENT_SCORE_PROPERTY = "OpponentScore";

        public MatchScorePM() {
            SetPlayerScoreProperty( 0 );
            SetOpponentScoreProperty( 0 );

            ListenForMessages( true );
        }

        public void Dispose() {
            ListenForMessages( false );
        }

        private void ListenForMessages( bool i_listen ) {
            if ( i_listen ) {
                MyMessenger.Instance.AddListener<MatchScoreUpdateData>( ClientMessages.UPDATE_SCORE, OnUpdateScore ); 
            } else {
                MyMessenger.Instance.RemoveListener<MatchScoreUpdateData>( ClientMessages.UPDATE_SCORE, OnUpdateScore );
            }
        }

        public void OnUpdateScore( MatchScoreUpdateData i_update ) {

        }

        private void SetPlayerScoreProperty( int i_score ) {
            ViewModel.SetProperty( PLAYER_SCORE_PROPERTY, i_score.ToString() );
        }

        private void SetOpponentScoreProperty( int i_score ) {
            ViewModel.SetProperty( OPPONENT_SCORE_PROPERTY, i_score.ToString() );
        }
    }
}
