using strange.extensions.command.impl;
using strange.extensions.signal.impl;
using PlayFab.ServerModels;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Qwf.Server {
    public class SendGameUpdatesToPlayersCommand : Command {
        [Inject] public IPlayerTurn ProcessedTurn { get; set; }
        [Inject] public LogSignal Logger { get; set; }

        public override void Execute() {
            Logger.Dispatch( LoggerTypes.Info, "Processed player turn for " + ProcessedTurn.GetPlayer().Id );


        }
    }
}
