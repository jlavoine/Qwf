#if ENABLE_PLAYFABSERVER_API
using strange.extensions.command.impl;
using strange.extensions.signal.impl;
using PlayFab.ServerModels;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Networking.NetworkSystem;

namespace Qwf.Server {
    public class SendGamePlayerHandSignal : Signal<IGamePlayer> { }

    public class SendGamePlayerHandCommand : Command {
        [Inject] public IGamePlayer Player { get; set; }
        [Inject] public LogSignal Logger { get; set; }
        [Inject] public UnityNetworkingData UnityNetworkingData { get; set; }

        public override void Execute() {
            Logger.Dispatch( LoggerTypes.Info, "Sending hand to " + Player.Id );

            var connection = UnityNetworkingData.Connections.Find( c => c.PlayFabId == Player.Id );
            if ( connection != null && connection.IsAuthenticated ) {
                PlayerHandUpdateData updateData = PlayerHandUpdateData.Create( Player );
                string json = JsonConvert.SerializeObject( updateData );
                connection.Connection.Send( NetworkMessages.UpdatePlayerHand, new StringMessage() {
                    value = json
                } );
            }
        }
    }
}
#endif