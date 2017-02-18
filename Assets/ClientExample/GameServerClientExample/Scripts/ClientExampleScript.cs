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
#if UNITY_ANDROID && !UNITY_EDITOR

        AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = up.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject contentResolver = currentActivity.Call<AndroidJavaObject>("getContentResolver");
        AndroidJavaClass secure = new AndroidJavaClass("android.provider.Settings$Secure");
        string androidId = secure.CallStatic<string>("getString", contentResolver, "android_id");

        PlayFabClientAPI.LoginWithAndroidDeviceID(new LoginWithAndroidDeviceIDRequest() {
            TitleId = TitleId,
            AndroidDevice = SystemInfo.deviceModel,
            AndroidDeviceId = androidId,
            OS = SystemInfo.operatingSystem,
            CreateAccount = true
        }, (result) => {
            PlayFabId = result.PlayFabId;
            SessionTicket = result.SessionTicket;

            UnityEngine.Debug.Log("PlayFab Logged In Successfully");
            StartText.text = "PlayFab Logged In Successfully";
            //If you want to test locally where you are running the server in the Unity Editor
            if (IsLocalNetwork) {
                ConnectNetworkClient();
            }
            else {
                PlayFabClientAPI.Matchmake(new MatchmakeRequest()
                {
                    BuildVersion = BuildVersion,
                    GameMode = GameMode,
                    Region = GameRegion
                }, (matchMakeResult) =>
                {
                    int port = matchMakeResult.ServerPort ?? 7777;
                    GameServerAuthTicket = matchMakeResult.Ticket;
                    ConnectNetworkClient(matchMakeResult.ServerHostname, port);
                }, PlayFabErrorHandler.HandlePlayFabError);
                        
            }
        }, PlayFabErrorHandler.HandlePlayFabError);
#else
            PlayFabId = BackendManager.Instance.GetBackend<PlayFabBackend>().PlayerId;
            SessionTicket = BackendManager.Instance.GetBackend<PlayFabBackend>().SessionTicket;

            StartText.text = "PlayFab Logged In Successfully";
     
            if ( IsLocalNetwork ) {
                ConnectNetworkClient();
            } else {
                SendMatchMakeRequest();
            }
#endif
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
            StartText.text = "Connected, waiting for Authorization";
            UnityEngine.Debug.Log( "Network Client connected, You have 30 seconds to Authenticate or you get booted by the server." );
            UnityEngine.Debug.Log( "Authing with " + GameServerAuthTicket + " or " + SessionTicket );
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