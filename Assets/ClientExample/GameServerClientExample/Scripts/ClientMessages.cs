/// <summary>
/// Messages FOR the client, usually FROM the server.
/// </summary>

namespace Qwf {
    public static class ClientMessages {
        // playing the game
        public const string UPDATE_HAND = "UpdatePlayerHand";
        public const string UPDATE_SCORE = "UpdateMatchScore";
        public const string UPDATE_OBSTACLES = "UpdateObstacles";
        public const string UPDATE_TURN = "UpdateTurn";        
        public const string GAME_OVER_UPDATE = "GameOverUpdate";

        // matchmaking related
        public const string NETWORK_CLIENT_CREATED = "ConnectedToServer";
        public const string GAME_READY = "GameReady";

        // sending to the server
        public const string SEND_TURN_TO_SERVER = "SendTurnToServer";
    }
}