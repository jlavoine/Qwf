using MyLibrary;
using UnityEngine.Networking.NetworkSystem;
using Newtonsoft.Json;

namespace Qwf {
    public class ClientRelay : CoreClientRelay {
        
        public ClientRelay() {           
            ListenForMessages( true );
        }

        public void Dispose() {
            ListenForMessages( false );
        }

        private void ListenForMessages( bool i_listen ) {
            if ( i_listen ) {
                MyMessenger.Instance.AddListener<ClientTurnAttempt>( ClientMessages.SEND_TURN_TO_SERVER, SendTurnToServer );
                MyMessenger.Instance.AddListener<IUnityNetworkWrapper>( ClientMessages.NETWORK_CLIENT_CREATED, OnNetworkClientCreated );
            }
            else {
                MyMessenger.Instance.RemoveListener<ClientTurnAttempt>( ClientMessages.SEND_TURN_TO_SERVER, SendTurnToServer );
                MyMessenger.Instance.RemoveListener<IUnityNetworkWrapper>( ClientMessages.NETWORK_CLIENT_CREATED, OnNetworkClientCreated );
            }
        }

        public void OnNetworkClientCreated( IUnityNetworkWrapper i_network ) {
            SetNetworkClient( i_network );
            RegisterServerMessageHandlers();
        }

        public void RegisterServerMessageHandlers() {
            RegisterMessageHandler<PlayerHandUpdateData, PlayerHandUpdateData>( NetworkMessages.UpdatePlayerHand, ClientMessages.UPDATE_HAND );
            RegisterMessageHandler<GameObstaclesUpdate, IGameObstaclesUpdate>( NetworkMessages.UpdateObstacles, ClientMessages.UPDATE_OBSTACLES );
            RegisterMessageHandler<TurnUpdate, ITurnUpdate>( NetworkMessages.UpdateTurn, ClientMessages.UPDATE_TURN );
            RegisterMessageHandler<MatchScoreUpdateData, IMatchScoreUpdateData>( NetworkMessages.UpdateScore, ClientMessages.UPDATE_SCORE );
            RegisterMessageHandler<GameOverUpdate, IGameOverUpdate>( NetworkMessages.UpdateGameOver, ClientMessages.GAME_OVER_UPDATE );
        }

        public void SendTurnToServer( ClientTurnAttempt i_turnAttempt ) {
            StringMessage message = new StringMessage();
            message.value = JsonConvert.SerializeObject( i_turnAttempt );

            mNetwork.SendMessage( NetworkMessages.SendTurn, message );
        }
    }
}
