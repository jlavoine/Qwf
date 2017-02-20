#if ENABLE_PLAYFABSERVER_API
using System;
using System.Collections;
using PlayFab.ServerModels;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using MyLibrary;
using Qwf.Server;

public class UnityNetworkManagerMediator : EventMediator {
    [Inject] public UnityNetworkManagerView View { get; set; }
    [Inject] public UnityNetworkingData UnityNetworkingData { get; set; }
    [Inject] public ServerSettingsData ServerSettingsData { get; set; }
    [Inject] public LogSignal Logger { get; set; }
    [Inject] public PlayFabServerShutdownSignal ShutDownSignal { get; set; }
    [Inject] public AuthenticateSessionTicketSignal AuthenticateSessionTicketSignal { get; set; }
    [Inject] public AuthenticateSessionTicketResponseSignal AuthenticateSessionTicketResponseSignal { get; set; }
    [Inject] public RedeemMatchmakerTicketSignal RedeemMatchmakerTicketSignal { get; set; }
    [Inject] public RedeemMatchmakerTicketResponseSignal RedeemMatchmakerTicketResponseSignal { get; set; }
    [Inject] public NotifyMatchmakerPlayerLeftSignal PlayerLeftSignal { get; set; }
    [Inject] public NotifyMatchmakerPlayerLeftResponseSignal PlayerLeftResponse { get; set; }
    [Inject] public ClientDisconnectedSignal ClientDisconnectedSignal { get; set; }

    [Inject] public CreateGamePlayerSignal CreateGamePlayerSignal { get; set; }

    public class AuthTicketMessage : MessageBase {
        public string PlayFabId;
        public string AuthTicket;
        public bool IsLocal;
    }

    public override void OnRegister() {
        NetworkServer.RegisterHandler(MsgType.Connect, OnServerConnect);
        NetworkServer.RegisterHandler(MsgType.Disconnect, OnServerDisconnect);
        NetworkServer.RegisterHandler(MsgType.Error, OnServerError);
        NetworkServer.RegisterHandler(CoreNetworkMessages.Authenticate, OnAuthenticateConnection);

        AuthenticateSessionTicketResponseSignal.AddListener( OnAuthLocalUserResponse );
        RedeemMatchmakerTicketResponseSignal.AddListener( OnAuthUserResponse );

        StartCoroutine(CheckForConnectionsOrClose());
    }

    IEnumerator CheckForConnectionsOrClose() {
        yield return new WaitForSeconds(UnityNetworkingData.MaxWaitForConnectSeconds);
        if (UnityNetworkingData.ConnectedClients == 0) {
            Logger.Dispatch(LoggerTypes.Info, "No Connections were made, shutting down.");
            ShutDownSignal.Dispatch();
        }
    }

    IEnumerator CheckForUnauthenticatedClients(int connectionId) {
        yield return new WaitForSeconds(UnityNetworkingData.MaxWaitForAuthSeconds);
        var uconn = UnityNetworkingData.Connections.Find(c => c.ConnectionId == connectionId);
        if (uconn != null) {
            if (!uconn.IsAuthenticated) {
                uconn.Connection.Disconnect();
            }
        }
    }

    private void OnAuthenticateConnection(NetworkMessage netMsg) {
        var uconn = UnityNetworkingData.Connections.Find(c => c.ConnectionId == netMsg.conn.connectionId);
        if (uconn != null) {
            var message = netMsg.ReadMessage<AuthTicketMessage>();
            uconn.PlayFabId = message.PlayFabId;
            Logger.Dispatch(LoggerTypes.Info, string.Format("Auth Received: PlayFabId:{0} AuthTicket:{1}", message.PlayFabId,message.AuthTicket));

            if (!message.IsLocal) {
                //RedeemMatchmakerTicketResponseSignal.AddOnce(OnAuthUserResponse); // bug from original code if multiple players auth at once
                RedeemMatchmakerTicketSignal.Dispatch(new RedeemMatchmakerTicketRequest() {
                    Ticket = message.AuthTicket,
                    LobbyId = ServerSettingsData.GameId.ToString()
                });
            } else {
                //AuthenticateSessionTicketResponseSignal.AddOnce(OnAuthLocalUserResponse); // bug from original code if multiple players auth at once
                AuthenticateSessionTicketSignal.Dispatch(new AuthenticateSessionTicketRequest() {
                    SessionTicket = message.AuthTicket
                });
            }
        }
    }

    private void OnAuthLocalUserResponse(AuthenticateSessionTicketResult response) {
        Logger.Dispatch(LoggerTypes.Info, string.Format("PlayFab Says AuthTicket isValid:{0}", true));
        
        var uconn = UnityNetworkingData.Connections.Find(c => c.PlayFabId == response.UserInfo.PlayFabId);
        if (uconn != null) {
            if ( uconn.IsAuthenticated == true ) {                
                return;
            }

            uconn.IsAuthenticated = true;

            // send the game player id here!!!
            uconn.Connection.Send(CoreNetworkMessages.OnAuthenticated, new StringMessage() {
                value = "Client Authenticated Successfully"
            });
            
            CreateGamePlayerSignal.Dispatch( response.UserInfo.PlayFabId );
        }

        RemoveListenersIfAllPlayersAuthed();
    }

    private void OnAuthUserResponse(RedeemMatchmakerTicketResult response) {
        Logger.Dispatch(LoggerTypes.Info, string.Format("PlayFab Says AuthTicket isValid:{0}",response.TicketIsValid));
        var uconn = UnityNetworkingData.Connections.Find(c => c.PlayFabId == response.UserInfo.PlayFabId);
        if (uconn != null) {
            if ( uconn.IsAuthenticated == true ) {
                return;
            }

            uconn.IsAuthenticated = response.TicketIsValid;
            uconn.Connection.Send(CoreNetworkMessages.OnAuthenticated, new StringMessage() {
                value = "Client Authenticated Successfully"
            });
            
            CreateGamePlayerSignal.Dispatch( response.UserInfo.PlayFabId);
        }

        RemoveListenersIfAllPlayersAuthed();
    }

    private void RemoveListenersIfAllPlayersAuthed() {
                int authCount = 0;
        foreach ( var conn in UnityNetworkingData.Connections ) {
            if ( conn.IsAuthenticated ) {
                authCount++;
            }
        }

        if ( authCount == 2 ) {
            AuthenticateSessionTicketResponseSignal.RemoveListener( OnAuthLocalUserResponse );
            RedeemMatchmakerTicketResponseSignal.RemoveListener( OnAuthUserResponse );
        }
    }

    // called when a client connects 
    private void OnServerConnect(NetworkMessage netMsg) {        
        UnityNetworkingData.ConnectedClients++;
        UnityNetworkingData.Connections.Add(new UnityNetworkingData.UnityNetworkConnection() {
            Connection = netMsg.conn,
            ConnectionId = netMsg.conn.connectionId,
            LobbyId = ServerSettingsData.GameId.ToString(),
            IsAuthenticated = false
        });

        //Security:
        //Give them 30 seconds to authenticate or close the connection.
        StartCoroutine(CheckForUnauthenticatedClients(netMsg.conn.connectionId));

        Logger.Dispatch(LoggerTypes.Info, "A Unity Client Connected");
    }

    // called when a client disconnects
    private void OnServerDisconnect(NetworkMessage netMsg) {
        if (UnityNetworkingData.ConnectedClients - 1 >= 0) {
            UnityNetworkingData.ConnectedClients--;
        }

        if (UnityNetworkingData.ConnectedClients == 0) {
            StartCoroutine(CheckForConnectionsOrClose());    
        }

        var connection = UnityNetworkingData.Connections.Find(c => c.ConnectionId == netMsg.conn.connectionId);
        if (connection != null) {
            if (connection.IsAuthenticated && ServerSettingsData.GameId > 0) {
                PlayerLeftResponse.AddOnce((playerLeftResponse) => {
                    ClientDisconnectedSignal.Dispatch(connection.ConnectionId, connection.PlayFabId);
                    Logger.Dispatch(LoggerTypes.Info,string.Format("Player Has Left:{0}",connection.PlayFabId));
                    UnityNetworkingData.Connections.Remove(connection);
                });

                PlayerLeftSignal.Dispatch(new NotifyMatchmakerPlayerLeftRequest() {
                    PlayFabId = connection.PlayFabId,
                    LobbyId = ServerSettingsData.GameId.ToString() 
                });   
            } else {
                ClientDisconnectedSignal.Dispatch(connection.ConnectionId,connection.PlayFabId);
                UnityNetworkingData.Connections.Remove(connection);
            }
        }

        Logger.Dispatch(LoggerTypes.Info, "A Unity Client Disconnected");
    }

    // called when a network error occurs
    public void OnServerError(NetworkMessage netMsg) {
        try {
            var error = netMsg.ReadMessage<ErrorMessage>();
            if (error != null) {
                Logger.Dispatch(LoggerTypes.Info,
                    string.Format("Unity Network Connection Status: code - {0}", error.errorCode));
            }
        } catch (Exception) {
            Logger.Dispatch(LoggerTypes.Info,"Unity Network Connection Status, but we could not get the reason, check the Unity Logs for more info.");
        }
        ShutDownSignal.Dispatch();
    }
}
#endif