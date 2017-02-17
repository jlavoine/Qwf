#if ENABLE_PLAYFABSERVER_API
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using PlayFab.ServerModels;
using Newtonsoft.Json;
using strange.extensions.signal.impl;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

namespace Qwf.Server {
    public class PlayerTurnProcessedSignal : Signal<IPlayerTurn> { }
    public class PostPlayerTurnProcessedSignal : Signal<IPlayerTurn> { }

    public class ClientTurnAttemptMediator : Mediator {        

        [Inject] public LogSignal Logger { get; set; }
        [Inject] public UnityNetworkingData UnityNetworkingData { get; set; }

        [Inject] public PlayerTurnProcessedSignal PlayerTurnProcessed { get; set; }
        [Inject] public PostPlayerTurnProcessedSignal PostPlayerTurnProcessed { get; set; }

        [Inject] public IGameManager GameManager { get; set; }

        public override void OnRegister() {
            Logger.Dispatch( LoggerTypes.Info, string.Format( "ClientTurnAttemptMediator.OnRegister()" ) );
            UnityEngine.Debug.LogError( "registered turn attempt mediator" );
            NetworkServer.RegisterHandler( NetworkMessages.SendTurn, OnClientTurnAttempt );
        }

        private void OnClientTurnAttempt( NetworkMessage i_message ) {
            if ( IsMessageFromConnectedPlayer( i_message ) && GameManager.IsReady() ) {
                StringMessage message = i_message.ReadMessage<StringMessage>();
                ClientTurnAttempt turnAttempt = JsonConvert.DeserializeObject<ClientTurnAttempt>( message.value );

                IPlayerTurn turn = CreateTurnFromAttempt( turnAttempt );
                TryToProcessPlayerTurn( turn );                
            }
        }

        private void TryToProcessPlayerTurn( IPlayerTurn i_turn ) {
            // this method does a bit much; I wish GameManager was officially part of Strange, so I could just dispatch an
            // event instead of checking here
            Logger.Dispatch( LoggerTypes.Info, string.Format( "Trying turn for " + i_turn.GetPlayer().Id ) );

            if ( GameManager.IsPlayerTurnValidForGameState( i_turn ) ) {
                GameManager.TryPlayerTurn( i_turn );
                PlayerTurnProcessed.Dispatch( i_turn );
                PostPlayerTurnProcessed.Dispatch( i_turn );
            }
        }

        private bool IsMessageFromConnectedPlayer( NetworkMessage i_message ) {
            var uconn = UnityNetworkingData.Connections.Find( c => c.ConnectionId == i_message.conn.connectionId );
            return uconn != null;
        }

        private IPlayerTurn CreateTurnFromAttempt( ClientTurnAttempt i_turnAttempt ) {
            IGamePlayer player = GameManager.GetPlayerFromId( i_turnAttempt.PlayerId );
            List<IGameMove> moves = new List<IGameMove>();

            if ( i_turnAttempt.MoveAttempts != null ) {
                foreach ( ClientMoveAttempt moveAttempt in i_turnAttempt.MoveAttempts ) {
                    Logger.Dispatch( LoggerTypes.Info, "Processing move attempt for piece " + moveAttempt.PieceInHandIndex + " on obstacle " + moveAttempt.ObstacleIndex + " on slot " + moveAttempt.ObstacleSlotIndex );
                    IServerGamePiece heldPiece = player.GetHeldPieceOfIndex( moveAttempt.PieceInHandIndex );
                    IGameObstacle targetObstacle = GameManager.Board.GetCurrentObstacleOfIndex( moveAttempt.ObstacleIndex );
                    IGamePieceSlot targetSlot = targetObstacle.GetSlotOfIndex( moveAttempt.ObstacleSlotIndex );

                    IGameMove move = new GameMove( heldPiece, targetObstacle, targetSlot );
                    moves.Add( move );
                }
            }

            IPlayerTurn turn = new PlayerTurn( player, moves );
            return turn;
        }
    }
}
#endif