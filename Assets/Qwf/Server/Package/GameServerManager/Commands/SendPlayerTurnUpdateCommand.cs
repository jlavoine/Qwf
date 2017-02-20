#if ENABLE_PLAYFABSERVER_API
using strange.extensions.command.impl;
using strange.extensions.signal.impl;
using Newtonsoft.Json;
using UnityEngine.Networking.NetworkSystem;

namespace Qwf.Server {
    public class SendPlayerTurnUpdateSignal : Signal<IGamePlayer> { }

    public class SendPlayerTurnUpdateCommand : Command {
        [Inject] public IGamePlayer Player { get; set; }
        [Inject] public LogSignal Logger { get; set; }
        [Inject] public UnityNetworkingData UnityNetworkingData { get; set; }

        public override void Execute() {
            Logger.Dispatch( LoggerTypes.Info, "Sending turn update with active player: " + Player.Id );
            
            foreach ( var uconn in UnityNetworkingData.Connections ) {
                bool isThisPlayerActive = uconn.PlayFabId == Player.Id;
                string updateJSON = CreateTurnUpdateJSON( isThisPlayerActive );

                uconn.Connection.Send( NetworkMessages.UpdateTurn, new StringMessage() {
                    value = updateJSON
                } );
            }
        }

        private string CreateTurnUpdateJSON( bool i_isPlayerActive ) {
            TurnUpdate update = new TurnUpdate();
            update.IsPlayerActive = i_isPlayerActive;
            string updateJSON = JsonConvert.SerializeObject( update );

            return updateJSON;
        }
    }
}
#endif