using System.Collections.Generic;
using strange.extensions.mediation.impl;
using PlayFab.ServerModels;
using Newtonsoft.Json;
using strange.extensions.signal.impl;

namespace Qwf.Server {
    public class GameBoardCreatedSignal : Signal<GameBoard> { }

    public class CreateGameBoardMediator : Mediator {
        [Inject] public CreateGameBoardView View { get; set; }

        [Inject] public LogSignal Logger { get; set; }

        [Inject] public IGameRules GameRules { get; set; }

        [Inject] public GetTitleDataSignal GetTitleDataSignal { get; set; }
        [Inject] public GetTitleDataResponseSignal GetTitleDataResponseSignal { get; set; }

        public override void OnRegister() {
            Logger.Dispatch( LoggerTypes.Info, string.Format( "CreateGameBoardMediator.OnRegister()" ) );

            GetTitleDataSignal.Dispatch( new GetTitleDataRequest() {
                Keys = new List<string> { "GameObstacles" }
            } );

            GetTitleDataResponseSignal.AddOnce( ( result ) => {
                foreach ( KeyValuePair<string, string> kvp in result.Data ) {
                    UnityEngine.Debug.Log( "Key is " + kvp.Key + " and result is " + kvp.Value );
                    CreateGameBoard( kvp.Value );
                }
            } );
        }

        private void CreateGameBoard( string i_obstacles ) {
            UnityEngine.Debug.LogError( "What's the string: " + i_obstacles );
            List<GameObstacleData> allObstacleData = JsonConvert.DeserializeObject<List<GameObstacleData>>( i_obstacles );
            List<IGameObstacle> allObstacles = new List<IGameObstacle>();

            foreach ( GameObstacleData obstacleData in allObstacleData ) {
                List<IGamePieceSlot> slots = CreateSlotsFromObstacle( obstacleData );
                IGameObstacle obstacle = new GameObstacle( slots, obstacleData.FinalBlowValue );
                allObstacles.Add( obstacle );
            }

            GameBoard board = new GameBoard( allObstacles, GameRules.GetMaxCurrentObstacles() );
        }

        private List<IGamePieceSlot> CreateSlotsFromObstacle( GameObstacleData i_obstacleData ) {
            List<IGamePieceSlot> slots = new List<IGamePieceSlot>();

            foreach ( IGamePieceSlotData slotData in i_obstacleData.SlotsData ) {
                IGamePieceSlot slot = new GamePieceSlot( slotData );
                slots.Add( slot );
            }

            return slots;
        }
    }
}
