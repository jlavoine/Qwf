using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.UI;
using Region = PlayFab.ClientModels.Region;
using Qwf;

namespace MyLibrary {
    public class ClientExampleScript : MonoBehaviour {
        public bool IsLocalNetwork;
        public string host = "localhost";
        public int port = 7777;
        public string TitleId;
        public string PlayFabId;
        public string BuildVersion;
        public string GameMode;
        public Region GameRegion;

        //Note I would not normally make the session ticket public.
        public string SessionTicket;
        public string GameServerAuthTicket;

        public Text Header;
        public Text Message;
        public Text StartText;

        public NetworkClient _network;

        private ClientRelay mGameRelay;

        void Start() {            
            StartText.text = "Loading...";

            if ( string.IsNullOrEmpty( TitleId ) ) {
                UnityEngine.Debug.LogError( "Please Enter your Title Id on the ClientExampleGameObject" );
                return;
            }

            PlayFabSettings.TitleId = TitleId;
            PlayFabId = BackendManager.Instance.GetBackend<PlayFabBackend>().PlayerId;
            SessionTicket = BackendManager.Instance.GetBackend<PlayFabBackend>().SessionTicket;

            StartText.text = "PlayFab Logged In Successfully";
     
            if ( IsLocalNetwork ) {
                ConnectNetworkClient();
            } else {
                SendMatchMakeRequest();
            }
        }

        private void SendMatchMakeRequest() {
            UnityEngine.Debug.Log( "about to match make with " + BuildVersion + " - " + GameMode + " - " + GameRegion );
            PlayFabClientAPI.Matchmake( new MatchmakeRequest() {
                BuildVersion = BuildVersion,
                GameMode = GameMode,
                Region = GameRegion
            }, ( matchMakeResult ) => {
                int port = matchMakeResult.ServerPort ?? 7777;
                GameServerAuthTicket = matchMakeResult.Ticket;
                ConnectNetworkClient( matchMakeResult.ServerHostname, port );
            }, PlayFabErrorHandler.HandlePlayFabError );
        }

        void OnDestroy() {
            if ( mGameRelay != null ) {
                mGameRelay.Dispose();
            }
        }

        private void ConnectNetworkClient( string host = "localhost", int port = 7777 ) {
            _network = new NetworkClient();
            _network.RegisterHandler( MsgType.Connect, OnConnected );
            _network.RegisterHandler( CoreNetworkMessages.OnAuthenticated, OnAuthenticated );
            _network.RegisterHandler( MsgType.Error, OnClientNetworkingError );
            _network.RegisterHandler( MsgType.Disconnect, OnClientDisconnect );

            mGameRelay = new ClientRelay( new UnityNetworkWrapper( _network ) );
            mGameRelay.RegisterServerMessageHandlers();

            if ( IsLocalNetwork ) {
                host = this.host;
                port = this.port;
            }

            _network.Connect( host, port );
            UnityEngine.Debug.LogFormat( "Network Client Created, waiting for connection on ServerHost:{0} Port:{1}", host, port );
        }

        private void OnConnected( NetworkMessage netMsg ) {
            StartText.text =  "Connected, waiting for Authorization";

            _network.Send( CoreNetworkMessages.Authenticate, new AuthTicketMessage() {
                PlayFabId = PlayFabId,
                AuthTicket = !string.IsNullOrEmpty( GameServerAuthTicket ) ? GameServerAuthTicket : SessionTicket,
                IsLocal = IsLocalNetwork
            } );
        }

        private void OnAuthenticated( NetworkMessage netMsg ) {            
            StartText.text = "Ready";
        }

        private void OnClientNetworkingError( NetworkMessage netMsg ) {
            var errorMessage = netMsg.ReadMessage<ErrorMessage>();
            UnityEngine.Debug.LogErrorFormat( "Oops Something went wrong. ErrorCode:{0}", errorMessage.errorCode );
        }

        private void OnClientDisconnect( NetworkMessage netMsg ) {
            StartText.text = "Disconnected";
        }

        void ClearText() {
            StartText.text = "";
        }
    }
}