using System.Collections.Generic;
using strange.extensions.mediation.impl;
using PlayFab.ServerModels;
using Newtonsoft.Json;
using strange.extensions.signal.impl;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

namespace Qwf.Server {
    public class GameOverMediator : Mediator {
        [Inject] public LogSignal Logger { get; set; }
        [Inject] public PlayFabServerShutdownSignal ShutDownSignal { get; set; }
        [Inject] public UnityNetworkingData UnityNetworkingData { get; set; }

        [Inject] public PlayerTurnProcessedSignal PlayerTurnProcessed { get; set; }

        [Inject] public IGameManager GameManager { get; set; }
        [Inject] public IScoreKeeper ScoreKeeper { get; set; }


        public override void OnRegister() {
            Logger.Dispatch( LoggerTypes.Info, string.Format( "GameOverMediator.OnRegister()" ) );

            PlayerTurnProcessed.AddListener( OnTurnProcessed );
        }

        private void OnTurnProcessed( IPlayerTurn i_turn ) {
            if ( GameManager.IsGameOver() ) {                
                SendGameOverUpdateToClients();
                ShutDownServer();                
            }
        }

        private void SendGameOverUpdateToClients() {
            Logger.Dispatch( LoggerTypes.Info, "Game is over, sending update to players" );
            GameOverUpdate update = new GameOverUpdate();
            update.Winner = ScoreKeeper.GetWinner();

            string updateJSON = JsonConvert.SerializeObject( update );

            foreach ( var uconn in UnityNetworkingData.Connections ) {
                uconn.Connection.Send( NetworkMessages.UpdateGameOver, new StringMessage() {
                    value = updateJSON
                } );
            }
        }

        private void ShutDownServer() {
            Logger.Dispatch( LoggerTypes.Info, "Game is over, shutting down server" );
            ShutDownSignal.Dispatch();
        }
    }
}
