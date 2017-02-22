using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.UI;
using Region = PlayFab.ClientModels.Region;
using Qwf;

namespace MyLibrary {
    public class MatchMaker {
        private const string TITLE_ID = "FA22";
        private const string GAME_MODE = "Basic";
        private const string BUILD_VERSION = "QwF_Server_1";
        private const Region REGION = Region.USEast;

        private const string LOCAL_HOST = "localhost";
        private const int LOCAL_PORT = 7777;

        private NetworkClient _network;
        private string PlayFabId;
        private string SessionTicket;
        private string GameServerAuthTicket;

        private bool mIsLocal;

        public MatchMaker() {}

        public void BeginMatchMakingProcess( bool i_isLocal ) {
            mIsLocal = i_isLocal;

            PlayFabSettings.TitleId = TITLE_ID;
            PlayFabId = BackendManager.Instance.GetBackend<PlayFabBackend>().PlayerId;
            SessionTicket = BackendManager.Instance.GetBackend<PlayFabBackend>().SessionTicket;

            if ( i_isLocal ) {
                ConnectNetworkClient();
            }
            else {
                SendMatchMakeRequest();
            }
        }

        private void SendMatchMakeRequest() {
            UnityEngine.Debug.Log( "about to match make with " + BUILD_VERSION + " - " + GAME_MODE + " - " + REGION );
            PlayFabClientAPI.Matchmake( new MatchmakeRequest() {
                BuildVersion = BUILD_VERSION,
                GameMode = GAME_MODE,
                Region = REGION
            }, ( matchMakeResult ) => {
                int port = matchMakeResult.ServerPort ?? 7777;
                GameServerAuthTicket = matchMakeResult.Ticket;
                ConnectNetworkClient( matchMakeResult.ServerHostname, port );
            }, OnMatchMakeError );
        }

        private void ConnectNetworkClient( string host = "localhost", int port = 7777 ) {
            _network = new NetworkClient();
            _network.RegisterHandler( MsgType.Connect, OnConnected );
            _network.RegisterHandler( CoreNetworkMessages.OnAuthenticated, OnAuthenticated );
            _network.RegisterHandler( MsgType.Error, OnClientNetworkingError );
            _network.RegisterHandler( MsgType.Disconnect, OnClientDisconnect );
            _network.RegisterHandler( CoreNetworkMessages.GameReady, OnGameReady );

            MyMessenger.Instance.Send<IUnityNetworkWrapper>( ClientMessages.NETWORK_CLIENT_CREATED, new UnityNetworkWrapper( _network ) );

            if ( mIsLocal ) {
                host = LOCAL_HOST;
                port = LOCAL_PORT;
            }

            _network.Connect( host, port );
            UnityEngine.Debug.LogFormat( "Network Client Created, waiting for connection on ServerHost:{0} Port:{1}", host, port );
        }

        private void OnConnected( NetworkMessage netMsg ) {
            _network.Send( CoreNetworkMessages.Authenticate, new AuthTicketMessage() {
                PlayFabId = PlayFabId,
                AuthTicket = !string.IsNullOrEmpty( GameServerAuthTicket ) ? GameServerAuthTicket : SessionTicket,
                IsLocal = mIsLocal
            } );
        }

        private void OnAuthenticated( NetworkMessage netMsg ) {}

        private void OnGameReady( NetworkMessage netMsg ) {
            MyMessenger.Instance.Send( ClientMessages.GAME_READY );
        }

        private void OnClientNetworkingError( NetworkMessage netMsg ) {
            var errorMessage = netMsg.ReadMessage<ErrorMessage>();
            UnityEngine.Debug.LogErrorFormat( "Oops Something went wrong. ErrorCode:{0}", errorMessage.errorCode );
        }

        private void OnClientDisconnect( NetworkMessage netMsg ) {}

        private void OnMatchMakeError( PlayFabError error ) {
            MyMessenger.Instance.Send( ClientMessages.NO_SERVER_AVAILABLE );
        }
    }
}
