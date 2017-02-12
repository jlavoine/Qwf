/// <summary>
/// The actual numbers are arbitrary.
/// Network messages for the client and server to
/// communicate with one another.
/// </summary>

namespace Qwf {
    public static class NetworkMessages {
        // messages TO the client
        public static short UpdatePlayerHand = 5001;
        public static short UpdateObstacles = 5002;
        public static short UpdateTurn = 5003;
        public static short UpdateScore = 5004;

        // messages TO the server
        public static short SendTurn = 6003;
    }
}