using MyLibrary;

namespace Qwf.Client {
    public class PlayerHandGamePiecePM : GamePiecePM, IPlayerHandGamePiecePM {
        public const string CAN_MOVE_PROPERTY = "CanMovePiece";

        public PlayerHandGamePiecePM( IGamePieceData i_piece, string i_playerViewing ) : base( i_piece, i_playerViewing ) {
            ListenForMessages( true );

            SetCanMoveProperty( true );
        }

        public override void Dispose() {
            base.Dispose();

            ListenForMessages( false );
        }

        public void Play() {
            MyMessenger.Instance.Send( ClientGameEvents.MADE_MOVE );
            SetVisibility( false );
        }

        public void AttemptingToPlay() {            
            SetCanMoveProperty( false ); // this seems illogical; since the piece is already moving, it can't move (allowing its raycast to be off for drop events
        }

        public void InvalidPlayAttempt() {
            SetCanMoveProperty( true );
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
            SetCanMoveProperty( false );
        }

        private void SetCanMoveProperty( bool i_canMove ) {
            ViewModel.SetProperty( CAN_MOVE_PROPERTY, i_canMove );
        }
    }
}
