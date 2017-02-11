using strange.extensions.command.impl;
using strange.extensions.signal.impl;
using PlayFab.ServerModels;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Qwf.Server {
    public class CreateGamePlayerSignal : Signal<string> { }
    public class PlayerAddedSignal : Signal { }

    public class CreateGamePlayerCommand : Command {
        [Inject] public string PlayFabId { get; set; }
        [Inject] public LogSignal Logger { get; set; }
        [Inject] public IGameRules GameRules { get; set; }
        [Inject] public IScoreKeeper ScoreKeeper { get; set; }
        [Inject] public IGameManager GameManager { get; set; }

        [Inject] public SendGamePlayerHandSignal UpdatePlayerHandSignal { get; set; }
        [Inject] public PlayerAddedSignal PlayerAddedSignal { get; set; }

        [Inject] public GetTitleDataSignal GetTitleDataSignal { get; set; }
        [Inject] public GetTitleDataResponseSignal GetTitleDataResponseSignal { get; set; }

        public override void Execute() {
            Logger.Dispatch( LoggerTypes.Info, "Creating a player with id: " + PlayFabId );            

            GetTitleDataSignal.Dispatch( new GetTitleDataRequest() {
                Keys = new List<string> { "SampleDeck" }
            } );

            GetTitleDataResponseSignal.AddOnce( ( result ) => {
                foreach ( KeyValuePair<string, string> kvp in result.Data ) {
                    PlayerDeckData deckData = JsonConvert.DeserializeObject<PlayerDeckData>( kvp.Value );
                    IGamePlayer player = new GamePlayer( GameRules, deckData, PlayFabId );
                    ScoreKeeper.AddPlayer( player );
                    GameManager.AddPlayer( player, PlayFabId );
                    UpdatePlayerHandSignal.Dispatch( player );
                    PlayerAddedSignal.Dispatch();
                }
            } );
        }
    }
}
