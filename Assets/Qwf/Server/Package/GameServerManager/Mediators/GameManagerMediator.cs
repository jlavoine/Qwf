#if ENABLE_PLAYFABSERVER_API
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using PlayFab.ServerModels;
using Newtonsoft.Json;
using strange.extensions.signal.impl;
using System;
using UnityEngine.Networking.NetworkSystem;
using MyLibrary;

namespace Qwf.Server {
    public class GameManagerMediator : Mediator {
        [Inject] public LogSignal Logger { get; set; }
        [Inject] public UnityNetworkingData UnityNetworkingData { get; set; }

        [Inject] public GameManagerCreatedSignal GameManagerCreatedSignal { get; set; }

        [Inject] public IGameManager GameManager { get; set; }

        [Inject] public SendPlayerTurnUpdateSignal SendPlayerTurnUpdateSignal { get; set; }

        public override void OnRegister() {
            Logger.Dispatch( LoggerTypes.Info, string.Format( "GameManagerMediator.OnRegister()" ) );

            GameManagerCreatedSignal.AddOnce( OnGameManagerCreated );
        }

        private void OnGameManagerCreated() {
            Logger.Dispatch( LoggerTypes.Info, string.Format( "Received GameManager" ) );

            SendStartingDataToPlayers();
        }

        private void SendStartingDataToPlayers() {
            SendStartingObstacleData();
            PickAndSendStartingPlayer();
            SendGameReadyMessage();
        }

        private void SendStartingObstacleData() {
            GameObstaclesUpdate update = GetGameObstaclesUpdate();
            string updateJSON = JsonConvert.SerializeObject( update );

            foreach ( var uconn in UnityNetworkingData.Connections ) {
                Logger.Dispatch( LoggerTypes.Info, "Sending starting obstacle data to " + uconn.PlayFabId );
                uconn.Connection.Send( NetworkMessages.UpdateObstacles, new StringMessage() {
                    value = updateJSON
                } );
            }
        }

        private void PickAndSendStartingPlayer() {
            IGamePlayer startingPlayer = GameManager.ActivePlayer;
            SendPlayerTurnUpdateSignal.Dispatch( startingPlayer );
        }

        private GameObstaclesUpdate GetGameObstaclesUpdate() {
            IGameBoard board = GameManager.Board;
            GameObstaclesUpdate update = GameObstaclesUpdate.Create( board.GetCurrentObstacles() );

            return update;
        }

        private void SendGameReadyMessage() {
            foreach ( var uconn in UnityNetworkingData.Connections ) {
                Logger.Dispatch( LoggerTypes.Info, "Sending game ready " + uconn.PlayFabId );
                uconn.Connection.Send( CoreNetworkMessages.GameReady, new StringMessage() {
                    value = ""
                } );
            }
        }
    }
}
#endif