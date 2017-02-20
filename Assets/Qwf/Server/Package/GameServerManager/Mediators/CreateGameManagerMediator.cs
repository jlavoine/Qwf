#if ENABLE_PLAYFABSERVER_API
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using PlayFab.ServerModels;
using Newtonsoft.Json;
using strange.extensions.signal.impl;
using System;
using UnityEngine.Networking.NetworkSystem;

namespace Qwf.Server {
    public class GameManagerCreatedSignal : Signal { }

    public class CreateGameManagerMediator : Mediator {
        [Inject] public LogSignal Logger { get; set; }
        [Inject] public UnityNetworkingData UnityNetworkingData { get; set; }
        [Inject] public SetGameServerInstanceStateSignal SetGameServerInstanceStateSignal { get; set; }
        [Inject] public ServerSettingsData ServerSettingsData { get; set; }

        [Inject] public GameManagerCreatedSignal GameManagerCreatedSignal { get; set; }
        [Inject] public GameBoardCreatedSignal GameBoardCreatedSignal {get; set;}
        [Inject] public PlayerAddedSignal PlayerAddedSignal { get; set; }

        [Inject] public IScoreKeeper ScoreKeeper { get; set; }
        [Inject] public IGameManager GameManager { get; set; }

        public override void OnRegister() {
            Logger.Dispatch( LoggerTypes.Info, string.Format( "CreateGameManagerMediator.OnRegister()" ) );

            GameBoardCreatedSignal.AddOnce( OnGameBoardCreated );
            PlayerAddedSignal.AddListener( OnPlayerAdded );
        }

        private void OnGameBoardCreated( GameBoard i_board ) {
            GameManager.SetGameBoard( i_board );
        }

        private void OnPlayerAdded() {
            if ( AllPlayersJoined() ) {
                Logger.Dispatch( LoggerTypes.Info, string.Format( "All players have joined" ) );

                RemovePlayerAddedSignal();
                SetScoreKeeperOnGameManager();
                SendGameManagerCreatedSignal();
                RemoveServerFromMatchmaker();                                                              
            }
        }        

        private bool AllPlayersJoined() {
            return ScoreKeeper.GetNumPlayers() == 2;
        }

        private void RemovePlayerAddedSignal() {
            PlayerAddedSignal.RemoveListener( OnPlayerAdded );
        }

        private void SetScoreKeeperOnGameManager() {
            GameManager.SetScoreKeeper( ScoreKeeper );
        }

        private void SendGameManagerCreatedSignal() {
            GameManagerCreatedSignal.Dispatch();
        }

        private void RemoveServerFromMatchmaker() {
            Logger.Dispatch( LoggerTypes.Info, "Setting instance to closed for " + ServerSettingsData.GameId.ToString() );

            SetGameServerInstanceStateSignal.Dispatch( new SetGameServerInstanceStateRequest() {
                LobbyId = ServerSettingsData.GameId.ToString(),
                State = GameInstanceState.Closed
            } );
        }
    }
}
#endif