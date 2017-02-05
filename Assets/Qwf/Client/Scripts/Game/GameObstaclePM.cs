﻿using System;
using MyLibrary;
using System.Collections.Generic;

namespace Qwf.Client {
    public class GameObstaclePM : GenericViewModel, IGameObstaclePM {
        public const string IMAGE_PROPERTY = "ObstacleImage";
        public const string FINAL_BLOW_PROPERTY = "FinalBlowValue";
        public const string VISIBLE_PROPERTY = "IsVisible";

        public const string FINAL_BLOW_PREFIX = "+";

        private const int DEFAULT_SLOT_COUNT = 5; // TODO this should be pulled from a config file

        private List<GamePieceSlotPM> mSlotPiecePMs;
        public List<GamePieceSlotPM> SlotPiecePMs { get { return mSlotPiecePMs; } private set { mSlotPiecePMs = value; } }

        public GameObstaclePM( IGameObstacleUpdate i_data ) {
            SetProperties( i_data );
            CreateAllGamePieceSlotPMs( i_data );
        }

        public void Update( IGameObstacleUpdate i_data ) {
            SetProperties( i_data );
            UpdateSlotPMs( i_data );
        }

        public void SetProperties( IGameObstacleUpdate i_data ) {
            SetImageProperty( i_data );
            SetFinalBlowProperty( i_data );
            SetVisibility( true );
        }

        public void SetVisibility( bool i_visible ) {
            ViewModel.SetProperty( VISIBLE_PROPERTY, i_visible ? 1f : 0f );
        }

        private void SetImageProperty( IGameObstacleUpdate i_data ) {
            ViewModel.SetProperty( IMAGE_PROPERTY, i_data.GetImageKey() );
        }

        private void SetFinalBlowProperty( IGameObstacleUpdate i_data ) {
            ViewModel.SetProperty( FINAL_BLOW_PROPERTY, FINAL_BLOW_PREFIX + i_data.GetFinalBlowValue() );
        }

        private void CreateAllGamePieceSlotPMs( IGameObstacleUpdate i_data ) {
            SlotPiecePMs = new List<GamePieceSlotPM>();
            int slotCount = i_data.GetSlotCount();
            for ( int i = 0; i < DEFAULT_SLOT_COUNT; ++i ) {
                if ( i < slotCount ) {
                    AddSlotPiecePM( i_data.GetSlotUpdate( i ) );                    
                } else {
                    AddSlotPiecePM( null );
                }
            }
        }

        private void AddSlotPiecePM( IGamePieceSlotUpdate i_gamePieceSlotUpdate ) {
            SlotPiecePMs.Add( new GamePieceSlotPM( i_gamePieceSlotUpdate ) );
        }

        private void UpdateSlotPMs( IGameObstacleUpdate i_data ) {
            int slotCount = i_data.GetSlotCount();
            for ( int i = 0; i < SlotPiecePMs.Count; ++i ) {
                if ( i < slotCount ) {
                    SlotPiecePMs[i].SetProperties( i_data.GetSlotUpdate( i ) );
                } else {
                    SlotPiecePMs[i].SetVisibility( false );
                }
            }
        }
    }
}
