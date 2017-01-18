using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;

namespace Qwf.UnitTests {
    [TestFixture]
    public class TestGameManager {
        [Test]
        public void OnAttemptMove_IfAnyMoveIsNotLegal_NoMovesMade() {
            List<IGameMove> moves = new List<IGameMove>();
            moves.Add( GetMoveOfLegalStatus( true ) );
            moves.Add( GetMoveOfLegalStatus( false ) );

            GameManager systemUnderTest = new GameManager( Substitute.For<IGameBoard>() );
            systemUnderTest.AttemptMoves( Substitute.For<IGamePlayer>(), moves );

            foreach ( IGameMove move in moves ) {
                move.DidNotReceive().MakeMove();
            }
        }

        [Test]
        public void OnAttemptMove_IfAllMovesLegal_MovesAreMade() {
            List<IGameMove> moves = new List<IGameMove>();
            moves.Add( GetMoveOfLegalStatus( true ) );
            moves.Add( GetMoveOfLegalStatus( true ) );

            GameManager systemUnderTest = new GameManager( Substitute.For<IGameBoard>() );
            systemUnderTest.AttemptMoves( Substitute.For<IGamePlayer>(), moves );

            foreach ( IGameMove move in moves ) {
                move.Received( 1 ).MakeMove();
            }
        }

        [Test]
        public void OnLegalMoveAttempt_PlayerHasHandRefilled() {
            IGamePlayer mockPlayer = Substitute.For<IGamePlayer>();
            List<IGameMove> moves = new List<IGameMove>();
            moves.Add( GetMoveOfLegalStatus( true ) );

            GameManager systemUnderTest = new GameManager( Substitute.For<IGameBoard>() );
            systemUnderTest.AttemptMoves( mockPlayer, moves );

            mockPlayer.Received( 1 ).DrawToFillHand();
        }

        private IGameMove GetMoveOfLegalStatus( bool i_status ) {
            IGameMove move = Substitute.For<IGameMove>();
            move.IsLegal( Arg.Any<IGameBoard>() ).Returns( i_status );

            return move;
        }
    }
}
