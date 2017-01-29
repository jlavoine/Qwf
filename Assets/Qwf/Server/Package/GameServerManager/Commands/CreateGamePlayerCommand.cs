using UnityEngine;
using strange.extensions.command.impl;
using strange.extensions.signal.impl;
using UnityEngine.Networking;

namespace Qwf.Server {
    public class CreateGamePlayerSignal : Signal { }

    public class CreateGamePlayerCommand : Command {
        [Inject]
        public LogSignal Logger { get; set; }

        public override void Execute() {
            Logger.Dispatch( LoggerTypes.Info, "Creating a player." );
        }
    }
}
