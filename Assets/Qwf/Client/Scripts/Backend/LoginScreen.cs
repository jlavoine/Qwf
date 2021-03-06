﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MyLibrary;
using System.Collections;
using TMPro;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Qwf.Client {
    public class LoginScreen : MonoBehaviour {
        public const string STATUS_WAITING_TO_CHOOSE = "Choose player to login";
        public const string STATUS_CONNECTING = "Connecting to server...";
        public const string STATUS_DOWNLOADING_GAME = "Connected to server -- downloading game data!";
        public const string STATUS_DOWNLOADING_PLAYER = "Connected to server -- downloading player data!";
        public const string STATUS_FAILED = "Failed to connect to server. Please close and try again.";

        public GameObject LoginFailurePopup;

        private IQwfBackend mBackend;

        private bool mBackendFailure = false;

        private Login mLogin;   // is this the best way...?

        private AnalyticsTimer mLoginTimer = new AnalyticsTimer( LibraryAnalyticEvents.LOGIN_TIME, new MyTimer() );

        public GameObject PlayerSelectionArea;
        public TextMeshProUGUI LoginStatusText;

        void Start() {
            mBackend = new QwfBackend();
            BackendManager.Instance.Init( mBackend );

            MyMessenger.Instance.AddListener( BackendMessages.LOGIN_SUCCESS, OnLoginSuccess );
            MyMessenger.Instance.AddListener<IBackendFailure>( BackendMessages.BACKEND_REQUEST_FAIL, OnBackendFailure );

            LoginStatusText.text = STATUS_WAITING_TO_CHOOSE;
        }

        public void OnPlayerSelected_1() {
            OnPlayerSelected( "Player_1" ); // DF352B70F7C1B528
        }

        public void OnPlayerSelected_2() {
            OnPlayerSelected( "Player_2" ); // CDECF881F9EF2304
        }

        public void OnPlayerSelected_3() {
            OnPlayerSelected( "Player_3" ); // A9ED40432D085713
        }

        private void OnPlayerSelected(string player) {
            Destroy( PlayerSelectionArea );
            LoginStatusText.text = STATUS_CONNECTING;

            mLogin = new Login( mBackend, mLoginTimer, player );
            mLogin.Start();
        }

        private void DoneLoadingData() {
            if ( !mBackendFailure ) {
                LoginStatusText.gameObject.SetActive( false );
                mLoginTimer.StopAndSendAnalytic();

                LoadMainScene();
            }
        }

        private void LoadMainScene() {
            SceneManager.LoadScene( "Main" );
        }

        void OnDestroy() {
            if ( mLogin != null ) {
                mLogin.OnDestroy();
            }
           
            MyMessenger.Instance.RemoveListener( BackendMessages.LOGIN_SUCCESS, OnLoginSuccess );
            MyMessenger.Instance.RemoveListener<IBackendFailure>( BackendMessages.BACKEND_REQUEST_FAIL, OnBackendFailure );
        }

        private void OnBackendFailure( IBackendFailure i_failure ) {
            if ( !mBackendFailure ) {
                mBackendFailure = true;
                //gameObject.InstantiateUI( LoginFailurePopup );    // right now this conflicts with OutOfSync popup
                LoginStatusText.text = STATUS_FAILED;
            }
        }

        private void OnLoginSuccess() {
            StartCoroutine( LoadDataFromBackend() );
        }

        private IEnumerator LoadDataFromBackend() {
            LoginStatusText.text = STATUS_DOWNLOADING_GAME;
           
            StringTableManager.Instance.Init( "English", mBackend );
            PlayerManager.Instance.Init( new PlayerData() );

            //Constants.Init( mBackend );
            //GenericDataLoader.Init( mBackend );
            //GenericDataLoader.LoadDataOfClass<BuildingData>( GenericDataLoader.BUILDINGS );
            //GenericDataLoader.LoadDataOfClass<UnitData>( GenericDataLoader.UNITS );
            //GenericDataLoader.LoadDataOfClass<GuildData>( GenericDataLoader.GUILDS );

            while ( mBackend.IsBusy() ) {
                yield return 0;
            }
            mLoginTimer.StepComplete( LibraryAnalyticEvents.TITLE_DATA_TIME );

            //yield return SetUpPlayerData();
            
            DoneLoadingData();
        }

        private IEnumerator SetUpPlayerData() {
            LoginStatusText.text = STATUS_DOWNLOADING_PLAYER;

            // it's possible that the client is restarting and old player data exists -- we need to dispose of it
            /*if ( PlayerManager.Data != null ) {
                PlayerManager.Data.Dispose();
            }

            PlayerData playerData = new PlayerData();
            playerData.Init( mBackend );
            PlayerManager.Init( playerData );
            */
            while ( mBackend.IsBusy() ) {
                yield return 0;
            }
            /*
            playerData.AddDataStructures();
            playerData.CreateManagers();*/
            
            mLoginTimer.StepComplete( LibraryAnalyticEvents.INIT_PLAYER_TIME );
        }
    }
}