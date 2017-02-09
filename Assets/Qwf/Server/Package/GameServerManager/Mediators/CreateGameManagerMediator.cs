using System.Collections.Generic;
using strange.extensions.mediation.impl;
using PlayFab.ServerModels;
using Newtonsoft.Json;
using strange.extensions.signal.impl;
using System;
using UnityEngine.Networking.NetworkSystem;

namespace Qwf.Server {
    public class GameManagerCreatedSignal : Signal<GameManager> { }

    public class CreateGameManagerMediator : Mediator {
        [Inject] public LogSignal Logger { get; set; }
        [Inject] public UnityNetworkingData UnityNetworkingData { get; set; }

        [Inject] public GameManagerCreatedSignal GameManagerCreatedSignal { get; set; }
        [Inject] public GameBoardCreatedSignal GameBoardCreatedSignal {get; set;}
        [Inject] public PlayerAddedSignal PlayerAddedSignal { get; set; }

        [Inject] public IScoreKeeper ScoreKeeper { get; set; }

        private GameBoard mBoard;

        public override void OnRegister() {
            Logger.Dispatch( LoggerTypes.Info, string.Format( "CreateGameManagerMediator.OnRegister()" ) );

            GameBoardCreatedSignal.AddOnce( OnGameBoardCreated );
            PlayerAddedSignal.AddListener( OnPlayerAdded );
        }

        private void OnGameBoardCreated( GameBoard i_board ) {
            mBoard = i_board;
            CreateManagerIfReady();
        }

        private void OnPlayerAdded() {
            CreateManagerIfReady();
        }

        private void CreateManagerIfReady() {
            if ( mBoard != null && ScoreKeeper.GetNumPlayers() == 2 ) {
                PlayerAddedSignal.RemoveListener( OnPlayerAdded );
                Logger.Dispatch( LoggerTypes.Info, string.Format( "Everything is ready; creating game manager" ) );

                GameManager manager = new GameManager( mBoard, ScoreKeeper );
                GameManagerCreatedSignal.Dispatch( manager );
            }
        }
    }
}
