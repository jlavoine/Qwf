/// <summary>
/// Messages FOR the client, usually FROM the server.
/// </summary>

namespace Qwf {
    public static class ClientMessages {
        public const string UPDATE_HAND = "UpdatePlayerHand";
        public const string UPDATE_SCORE = "UpdateMatchScore";
        public const string UPDATE_OBSTACLES = "UpdateObstacles";
        public const string UPDATE_TURN = "UpdateTurn";
        public const string GAME_OVER_UPDATE = "GameOverUpdate";

        public const string SEND_TURN_TO_SERVER = "SendTurnToServer";
    }
}