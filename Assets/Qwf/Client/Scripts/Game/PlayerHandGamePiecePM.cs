using MyLibrary;

namespace Qwf.Client {
    public class PlayerHandGamePiecePM : GamePiecePM, IPlayerHandGamePiecePM {
        public const string CAN_MOVE_PROPERTY = "CanMovePiece";

        private int mIndex;
        public int Index { get { return mIndex; } set { mIndex = value; } }

        public PlayerHandGamePiecePM( IGamePieceData i_piece, string i_playerViewing ) : base( i_piece, i_playerViewing ) {
            ListenForMessages( true );

            SetCanMoveProperty( false );
        }

        public override void Dispose() {
            base.Dispose();

            ListenForMessages( false );
        }

        public void SetIndex( int i_index ) {
            Index = i_index;
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
                MyMessenger.Instance.AddListener( ClientGameEvents.RESET_MOVES, OnMovesReset );
                MyMessenger.Instance.AddListener<ITurnUpdate>( ClientMessages.UPDATE_TURN, OnTurnUpdate );
            }
            else {
                MyMessenger.Instance.RemoveListener( ClientGameEvents.MAX_MOVES_MADE, OnMaxMovesMade );
                MyMessenger.Instance.RemoveListener( ClientGameEvents.RESET_MOVES, OnMovesReset );
                MyMessenger.Instance.RemoveListener<ITurnUpdate>( ClientMessages.UPDATE_TURN, OnTurnUpdate );
            }
        }

        public void OnMaxMovesMade() {
            SetCanMoveProperty( false );
        }

        public void OnMovesReset() {
            if ( GamePiece != null ) {
                SetVisibility( true );
                SetCanMoveProperty( true );
            }
        }

        private void SetCanMoveProperty( bool i_canMove ) {
            ViewModel.SetProperty( CAN_MOVE_PROPERTY, i_canMove );
        }

        public void OnTurnUpdate( ITurnUpdate i_update ) {
            if ( i_update.IsThisPlayerActive() ) {
                SetCanMoveProperty( true );
            } else {
                SetCanMoveProperty( false );
            }
        }
    }
}
