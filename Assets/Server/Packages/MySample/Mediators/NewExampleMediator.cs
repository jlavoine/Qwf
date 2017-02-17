#if ENABLE_PLAYFABSERVER_API
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using PlayFab.ServerModels;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.Networking;

public class NewExampleMediator : Mediator {
    [Inject]
    public NewExampleView View { get; set; }

    [Inject]
    public GetTitleDataSignal GetTitleDataSignal { get; set; }
    [Inject]
    public GetTitleDataResponseSignal GetTitleDataResponseSignal { get; set; }

    [Inject]
    public UnityNetworkingData UnityNetworkingData { get; set; }

    [Inject]
    public PlayerJoinedSignal PlayerJoinedSignal { get; set; }

    [Inject]
    public ClientDisconnectedSignal ClientDisconnectedSignal { get; set; }

    [Inject]
    public LogSignal Logger { get; set; }

    public override void OnRegister() {
        GetTitleDataSignal.Dispatch( new GetTitleDataRequest() {
            Keys = new List<string> { "SampleDeck" }
        });

        GetTitleDataResponseSignal.AddOnce( ( result ) => {
            foreach (KeyValuePair<string, string> kvp in result.Data ) {
                UnityEngine.Debug.Log( "Key is " + kvp.Key + " and result is " + kvp.Value );
                //TestNetworking();
                //Logger.Dispatch( LoggerTypes.Info, string.Format( "Hey I got some title data" ) );
            }
        } );

        PlayerJoinedSignal.AddListener( ( result ) => {
            UnityEngine.Debug.Log( "Player joined, got this from my example)" );
            UnityEngine.Debug.Log( result.PlayFabId );
            Logger.Dispatch( LoggerTypes.Info, string.Format( "Hey did this work:{0}", result.PlayFabId ) );
        } );

        ClientDisconnectedSignal.AddListener( OnUserDisconnected );

        //NetworkServer.RegisterHandler( 200, OnAuthenticateConnection );
    }

    private void OnAuthenticateConnection( NetworkMessage netMsg ) {
        Logger.Dispatch( LoggerTypes.Info, string.Format( "Hey there was an auth" ) );
    }

    private void OnUserDisconnected( int connId, string playFabId ) {
        Logger.Dispatch( LoggerTypes.Info, string.Format( "Player disconnection", playFabId ) );
    }

    private void TestNetworking() {
        foreach ( var uconn in UnityNetworkingData.Connections ) {
            uconn.Connection.Send( 1000, new StringMessage() {
                value = "Hello everyone!"
            } );
        }
    }
}
#endif