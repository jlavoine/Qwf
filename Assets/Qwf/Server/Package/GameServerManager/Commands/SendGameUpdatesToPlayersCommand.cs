#if ENABLE_PLAYFABSERVER_API
using strange.extensions.command.impl;
using strange.extensions.signal.impl;
using PlayFab.ServerModels;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Networking.NetworkSystem;

namespace Qwf.Server {
    public class SendGameUpdatesToPlayersCommand : Command {
        [Inject] public IPlayerTurn ProcessedTurn { get; set; }
        [Inject] public LogSignal Logger { get; set; }

        [Inject] public IGameManager GameManager { get; set; }
        [Inject] public IScoreKeeper ScoreKeeper { get; set; }

        [Inject] public UnityNetworkingData UnityNetworkingData { get; set; }

        [Inject] public SendGamePlayerHandSignal UpdatePlayerHandSignal { get; set; }
        [Inject] public SendPlayerTurnUpdateSignal SendPlayerTurnUpdateSignal { get; set; }

        public override void Execute() {
            Logger.Dispatch( LoggerTypes.Info, "Processed player turn for " + ProcessedTurn.GetPlayer().Id );

            SendBoardUpdate();
            UpdatePlayerHandSignal.Dispatch( ProcessedTurn.GetPlayer() );
            SendPlayerTurnUpdateSignal.Dispatch( GameManager.ActivePlayer );
            SendScoreUpdate();            
        }

        private void SendBoardUpdate() {
            Logger.Dispatch( LoggerTypes.Info, "Sending board update" );
            GameObstaclesUpdate update = GameObstaclesUpdate.Create( GameManager.Board.GetCurrentObstacles() );
            string updateJSON = JsonConvert.SerializeObject( update );
            SendUpdate( NetworkMessages.UpdateObstacles, updateJSON );
        }

        private void SendScoreUpdate() {
            Logger.Dispatch( LoggerTypes.Info, "Sending score update" );
            MatchScoreUpdateData update = MatchScoreUpdateData.Create( GameManager.ActivePlayer, GameManager.InactivePlayer, ScoreKeeper );
            string updateJSON = JsonConvert.SerializeObject( update );
            SendUpdate( NetworkMessages.UpdateScore, updateJSON );
        }

        private void SendUpdate( short i_updateType, string i_updateJSON ) {
            foreach ( var uconn in UnityNetworkingData.Connections ) {                
                uconn.Connection.Send( i_updateType, new StringMessage() {
                    value = i_updateJSON
                } );
            }
        }
    }
}
#endif