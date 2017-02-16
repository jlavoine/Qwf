#if QWF_SERVER
using strange.extensions.command.impl;
using strange.extensions.signal.impl;
using PlayFab.ServerModels;
using System.Collections.Generic;
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

            TurnUpdate update = new TurnUpdate();
            update.ActivePlayer = Player.Id;

            string updateJSON = JsonConvert.SerializeObject( update );

            foreach ( var uconn in UnityNetworkingData.Connections ) {
                uconn.Connection.Send( NetworkMessages.UpdateTurn, new StringMessage() {
                    value = updateJSON
                } );
            }
        }
    }
}
#endif