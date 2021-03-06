﻿using MyLibrary;
using System.Collections.Generic;
using System;

namespace Qwf.Client {
    public class PlayerHandPM : GenericViewModel {
        private string m_id;

        public List<PlayerHandGamePiecePM> mGamePiecePMs;
        public List<PlayerHandGamePiecePM> GamePiecePMs { get { return mGamePiecePMs; } }

        private IGameRules mRules = new GameRules();

        public PlayerHandPM( List<IGamePieceData> i_gamePieces, string i_playerID ) {
            m_id = i_playerID;

            CreateGamePiecePMs( i_gamePieces, i_playerID );
        
            ListenForMessages( true );
        }

        public void Dispose() {
            ListenForMessages( false );

            foreach ( IGamePiecePM pm in GamePiecePMs ) {
                pm.Dispose();
            }
        }

        private void ListenForMessages( bool i_shouldListen ) {
            if ( i_shouldListen ) {
                MyMessenger.Instance.AddListener<PlayerHandUpdateData>( ClientMessages.UPDATE_HAND, OnUpdate );
            } else {
                MyMessenger.Instance.RemoveListener<PlayerHandUpdateData>( ClientMessages.UPDATE_HAND, OnUpdate );
            }
        }

        private void CreateGamePiecePMs( List<IGamePieceData> i_gamePieces, string i_playerID ) { 
            mGamePiecePMs = new List<PlayerHandGamePiecePM>();
            foreach ( IGamePieceData gamePiece in i_gamePieces ) {
                mGamePiecePMs.Add( new PlayerHandGamePiecePM( gamePiece, i_playerID ) );
            }

            AddMissingGamePiecesIfNecessary( i_playerID );
        }

        private void AddMissingGamePiecesIfNecessary( string i_playerID ) {
            for ( int i = mGamePiecePMs.Count; i < mRules.GetPlayerHandSize(); ++i ) {
                mGamePiecePMs.Add( new PlayerHandGamePiecePM( null, i_playerID ) );
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
                    mGamePiecePMs[i].SetIndex( i );
                }
                else {
                    mGamePiecePMs[i].SetVisibility( false );
                }
            }
        }
    }
}
