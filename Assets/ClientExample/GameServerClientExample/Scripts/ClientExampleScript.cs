using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.Json;
using PlayFab.PlayStreamModels;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.UI;
using Region = PlayFab.ClientModels.Region;
using MyLibrary;
using Qwf;

public class ClientExampleScript : MonoBehaviour
{
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

    public class GameServerMsgTypes
    {
        public const short Authenticate = 200;    //this is a custom message type defined on the server.
        public const short OnAuthenticated = 201; //this is a custom response message type defined on the server


        public static short RegisterForEvents = 300;
        public static short OnPlayStreamEventReceived = 301;
        public static short RequestForFriendList = 302;
        public static short OnSendFriendsList = 303;

        // These are custom defined message types that are defined on the server
        // You would normally have more of these custom to your game and this is 
        // an example of how to use these custom messages to communicate with your server
        // via unity networking.
        public const short MsgRecieverExample = 1001;
        public const short MsgRecieverExampleResponse = 1002;

        //Defined in the Server Title news Update Example.
        public const short OnTitleNewsUpdate = 1010;

    }

    public class AuthTicketMessage : MessageBase
    {
        public string PlayFabId;
        public string AuthTicket;
        public bool IsLocal;
    }

    // Use this for initialization
    void Start()
    {
        StartText.text = "Loading...";

        if (string.IsNullOrEmpty(TitleId))
        {
            UnityEngine.Debug.LogError("Please Enter your Title Id on the ClientExampleGameObject");
            return;
        }

        PlayFabSettings.TitleId = TitleId;
#if UNITY_ANDROID && !UNITY_EDITOR

        AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = up.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject contentResolver = currentActivity.Call<AndroidJavaObject>("getContentResolver");
        AndroidJavaClass secure = new AndroidJavaClass("android.provider.Settings$Secure");
        string androidId = secure.CallStatic<string>("getString", contentResolver, "android_id");

        PlayFabClientAPI.LoginWithAndroidDeviceID(new LoginWithAndroidDeviceIDRequest() 
        {
            TitleId = TitleId,
            AndroidDevice = SystemInfo.deviceModel,
            AndroidDeviceId = androidId,
            OS = SystemInfo.operatingSystem,
            CreateAccount = true
        }, (result) =>
        {
            PlayFabId = result.PlayFabId;
            SessionTicket = result.SessionTicket;

            UnityEngine.Debug.Log("PlayFab Logged In Successfully");
            StartText.text = "PlayFab Logged In Successfully";
            //If you want to test locally where you are running the server in the Unity Editor
            if (IsLocalNetwork)
            {
                ConnectNetworkClient();
            }
            else
            {
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
        PlayFabId = BackendManager.GetBackend<PlayFabBackend>().PlayFabId;
        SessionTicket = BackendManager.GetBackend<PlayFabBackend>().SessionTicket;

        UnityEngine.Debug.Log("PlayFab Logged In Successfully");
        StartText.text = "PlayFab Logged In Successfully";
        //If you want to test locally where you are running the server in the Unity Editor
        if ( IsLocalNetwork ) {
            ConnectNetworkClient();
        }
        else {
            UnityEngine.Debug.Log( "about to match make with " + BuildVersion + " - " + GameMode + " - " + GameRegion );
            PlayFabClientAPI.Matchmake( new MatchmakeRequest() {
                BuildVersion = BuildVersion,
                GameMode = GameMode,
                Region = GameRegion
            }, ( matchMakeResult ) => {
                UnityEngine.Debug.Log( "got a result!" );
                int port = matchMakeResult.ServerPort ?? 7777;
                GameServerAuthTicket = matchMakeResult.Ticket;
                ConnectNetworkClient( matchMakeResult.ServerHostname, port );
            }, PlayFabErrorHandler.HandlePlayFabError );
        }
#endif

    }

    private void ConnectNetworkClient(string host = "localhost", int port = 7777)
    {
        //Basic Unity Networking Client, note there are other ways to do this
        //Referr to the unity documentation on Unity Networking for more info.
        _network = new NetworkClient();
        _network.RegisterHandler(MsgType.Connect, OnConnected);
        _network.RegisterHandler(GameServerMsgTypes.OnAuthenticated, OnAuthenticated);
        _network.RegisterHandler(GameServerMsgTypes.MsgRecieverExampleResponse, OnMsgRecieverExampleResponse);
        _network.RegisterHandler(MsgType.Error, OnClientNetworkingError);
        _network.RegisterHandler(MsgType.Disconnect, OnClientDisconnect);
        _network.RegisterHandler( NetworkMessages.UpdatePlayerHand, OnUpdatePlayerHand );

        //If this fails, it will automatically disconnect from the server.
        if (IsLocalNetwork)
        {
            host = this.host;
            port = this.port;
        }
        _network.Connect(host, port);
        UnityEngine.Debug.LogFormat("Network Client Created, waiting for connection on ServerHost:{0} Port:{1}", host, port);

        /*  I wanted to expand on the statement above,  Unity Networking has a NetworkingManager that is a 
         *  monobehaviour that you can instantiate into the scene and it also has a HUD for debugging, 
         *  Some like to use that over the direct client.  So instead of creating a NetworkingClient
         *  You could just instantiate a NetworkManager Game Object Prefab, and get a refrence to it's Client
         *  property.  This would allow you to virtually do the same code above, but on that game object.
         *  For this example, I'm taking the most simple and direct path.
         */
    }

    private void OnUpdatePlayerHand(NetworkMessage netMsg) {
        StringMessage message = netMsg.ReadMessage<StringMessage>();
        UnityEngine.Debug.LogError( "The player's hand update: " + message.value );
    }

    private void OnConnected(NetworkMessage netMsg) {
        StartText.text = "Connected, waiting for Authorization";
        UnityEngine.Debug.Log("Network Client connected, You have 30 seconds to Authenticate or you get booted by the server.");
        UnityEngine.Debug.Log( "Authing with " + GameServerAuthTicket + " or " + SessionTicket );
        _network.Send(GameServerMsgTypes.Authenticate, new AuthTicketMessage()
        {
            PlayFabId = PlayFabId,
            AuthTicket = !string.IsNullOrEmpty(GameServerAuthTicket) ? GameServerAuthTicket : SessionTicket,
            IsLocal = IsLocalNetwork
        });
    }

    private void OnAuthenticated(NetworkMessage netMsg) {
        StartText.text = "Ready";
        UnityEngine.Debug.Log("Sending Custom Message to the server, telling it to do something ");
        //_network.Send(GameServerMsgTypes.MsgRecieverExample, new StringMessage());

        //_network.Send(GameServerMsgTypes.RegisterForEvents, new RegisterForEventsMessage() { Subscription = new PlayStreamSubscription() { EventName = "player_inventory_item_added", PlayFabId = PlayFabId } });
    }

    private void OnMsgRecieverExampleResponse(NetworkMessage netMsg)
    {
        /* Here we are passing back a string message from the server.
         * you can pass any types you want as long as they Match on the  server and the client
         * and they also have to extend MessageBase.  The nice thing about that is that
         * Unity Networking handles serializing and deserializing the data for you.
         * 
         * Here I am keeping it simple and passing a String message back from the server.
         */
        var message = netMsg.ReadMessage<StringMessage>();
        UnityEngine.Debug.Log(message.value);
    }

    private void OnClientNetworkingError(NetworkMessage netMsg)
    {
        var errorMessage = netMsg.ReadMessage<ErrorMessage>();
        UnityEngine.Debug.LogErrorFormat("Oops Something went wrong. ErrorCode:{0}", errorMessage.errorCode);
    }

    private void OnClientDisconnect(NetworkMessage netMsg)
    {
        UnityEngine.Debug.Log("Diconnected From Server");
        StartText.text = "Disconnected";
        //TODO: Find out why it disconnected, and retry if needed.
    }

    void ClearText()
    {
        StartText.text = "";
    }


    public void QuitButton()
    {
        Application.Quit();
    }

}
