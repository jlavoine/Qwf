/// <summary>
/// Messages FOR the client and FROM the client, usually as the
/// user takes actions.
/// </summary>
/// 
namespace Qwf.Client {
    public static class ClientGameEvents {
        public const string MADE_MOVE = "MadeMove";
        public const string MAX_MOVES_MADE = "MaxMovesMade";
        public const string RESET_MOVES = "ResetMoves";   
    }
}
