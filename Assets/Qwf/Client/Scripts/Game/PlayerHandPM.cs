using MyLibrary;
using System.Collections.Generic;

namespace Qwf.Client {
    public class PlayerHandPM : GenericViewModel {
        private string m_id;

        public List<IGamePiecePM> mGamePiecePMs;
        public List<IGamePiecePM> GamePiecePMs { get { return mGamePiecePMs; } }

        public PlayerHandPM( List<IGamePieceData> i_gamePieces, string i_playerID ) {
            m_id = i_playerID;

            CreateGamePiecePMs( i_gamePieces, i_playerID );
        
            ListenForMessages( true );
        }

        public void Dispose() {
            ListenForMessages( false );
        }

        private void ListenForMessages( bool i_shouldListen ) {
            if ( i_shouldListen ) {
                MyMessenger.Instance.AddListener<PlayerHandUpdateData>( ClientMessages.UPDATE_HAND, OnUpdate );
            } else {
                MyMessenger.Instance.RemoveListener<PlayerHandUpdateData>( ClientMessages.UPDATE_HAND, OnUpdate );
            }
        }

        private void CreateGamePiecePMs( List<IGamePieceData> i_gamePieces, string i_playerID ) { 
            mGamePiecePMs = new List<IGamePiecePM>();
            foreach ( IGamePieceData gamePiece in i_gamePieces ) {
                mGamePiecePMs.Add( new GamePiecePM( gamePiece, i_playerID ));
            }
        }

        public void OnUpdate( PlayerHandUpdateData i_data ) {
            if ( UpdateBelongsToPlayer( i_data ) ) {
                ProcessUpdateData( i_data );
            }
        }

        private bool UpdateBelongsToPlayer( PlayerHandUpdateData i_data ) {
            return m_id == i_data.Id;
        }

        private void ProcessUpdateData( PlayerHandUpdateData i_data ) {
            List<GamePieceData> newPieceList = i_data.GamePieces;
            for ( int i = 0; i < mGamePiecePMs.Count; ++i ) {
                if ( i < newPieceList.Count ) {
                    mGamePiecePMs[i].SetProperties( newPieceList[i] );
                }
                else {
                    mGamePiecePMs[i].SetVisibility( false );
                }
            }
        }
    }
}
