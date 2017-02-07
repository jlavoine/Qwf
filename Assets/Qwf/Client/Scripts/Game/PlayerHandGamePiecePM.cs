using MyLibrary;

namespace Qwf.Client {
    public class PlayerHandGamePiecePM : GamePiecePM {
        public const string CAN_MOVE_PROPERTY = "CanMovePiece";

        public PlayerHandGamePiecePM( IGamePieceData i_piece, string i_playerViewing ) : base( i_piece, i_playerViewing ) {
            ListenForMessages( true );
        }

        public override void Dispose() {
            base.Dispose();

            ListenForMessages( false );
        }

        private void ListenForMessages( bool i_listen ) {
            if ( i_listen ) {
                MyMessenger.Instance.AddListener( ClientGameEvents.MAX_MOVES_MADE, OnMaxMovesMade );
            }
            else {
                MyMessenger.Instance.RemoveListener( ClientGameEvents.MAX_MOVES_MADE, OnMaxMovesMade );
            }
        }

        public void OnMaxMovesMade() {

        }
    }
}
