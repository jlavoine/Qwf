using System.Collections.Generic;
using strange.extensions.mediation.impl;
using PlayFab.ServerModels;
using Newtonsoft.Json;
using strange.extensions.signal.impl;
using System;
using UnityEngine.Networking.NetworkSystem;

namespace Qwf.Server {
    public class GameManagerMediator : Mediator {
        [Inject] public LogSignal Logger { get; set; }
        [Inject] public UnityNetworkingData UnityNetworkingData { get; set; }

        [Inject] public GameManagerCreatedSignal GameManagerCreatedSignal { get; set; }

        private GameManager mManager;

        public override void OnRegister() {
            Logger.Dispatch( LoggerTypes.Info, string.Format( "GameManagerMediator.OnRegister()" ) );

            GameManagerCreatedSignal.AddOnce( OnGameManagerCreated );
        }

        private void OnGameManagerCreated( GameManager i_manager ) {
            Logger.Dispatch( LoggerTypes.Info, string.Format( "Received GameManager" ) );

            mManager = i_manager;

            SendStartingDataToPlayers();
        }

        private void SendStartingDataToPlayers() {
            SendStartingObstacleData();
            PickAndSendStartingPlayer();
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
            string startingPlayer = UnityNetworkingData.Connections[0].PlayFabId;
            TurnUpdate update = new TurnUpdate();
            update.ActivePlayer = startingPlayer;

            string updateJSON = JsonConvert.SerializeObject( update );

            foreach ( var uconn in UnityNetworkingData.Connections ) {
                Logger.Dispatch( LoggerTypes.Info, "Sending starting turn update data to " + uconn.PlayFabId );
                uconn.Connection.Send( NetworkMessages.UpdateTurn, new StringMessage() {
                    value = updateJSON
                } );
            }
        }

        private GameObstaclesUpdate GetGameObstaclesUpdate() {
            IGameBoard board = mManager.GameBoard;
            GameObstaclesUpdate update = GameObstaclesUpdate.GetUpdate( board.GetCurrentObstacles() );

            return update;
        }
    }
}
