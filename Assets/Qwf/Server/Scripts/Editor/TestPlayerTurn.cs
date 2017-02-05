using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;

namespace Qwf {
    [TestFixture]
    public class TestPlayerTurn {

        [Test]
        public void IfAnyMoveIsNotLegal_PlayerTurnNotValid() {
            List<IGameMove> moves = new List<IGameMove>();
            moves.Add( GetMoveOfLegalStatus( true ) );
            moves.Add( GetMoveOfLegalStatus( false ) );

            PlayerTurn systemUnderTest = new PlayerTurn( Substitute.For<IGamePlayer>(), moves );

            bool isValid = systemUnderTest.IsValid( Substitute.For<IGameBoard>() );
            Assert.IsFalse( isValid );
        }

        [Test]
        public void IfAllMovesLegal_PlayerTurnIsValid() {
            List<IGameMove> moves = new List<IGameMove>();
            moves.Add( GetMoveOfLegalStatus( true ) );
            moves.Add( GetMoveOfLegalStatus( true ) );

            PlayerTurn systemUnderTest = new PlayerTurn( Substitute.For<IGamePlayer>(), moves );

            bool isValid = systemUnderTest.IsValid( Substitute.For<IGameBoard>() );
            Assert.IsTrue( isValid );
        }

        [Test]
        public void IfMoveContainsDuplicatePieces_PlayerTurnNotValid() {
            List<IGameMove> moves = new List<IGameMove>();
            IGameMove sameMove = GetMoveOfLegalStatus( true );
            sameMove.GetTargetPiece().Returns( Substitute.For<IServerGamePiece>() );
            moves.Add( sameMove );
            moves.Add( sameMove );
            moves.Add( sameMove );

            PlayerTurn systemUnderTest = new PlayerTurn( Substitute.For<IGamePlayer>(), moves );

            bool isValid = systemUnderTest.IsValid( Substitute.For<IGameBoard>() );
            Assert.IsFalse( isValid );
        }

        [Test]
        public void GetPlayerReturnsPlayerOwnsTurn() {
            IGamePlayer mockPlayer = Substitute.For<IGamePlayer>();

            PlayerTurn systemUnderTest = new PlayerTurn( mockPlayer, new List<IGameMove>() );

            IGamePlayer turnOwner = systemUnderTest.GetPlayer();

            Assert.AreEqual( mockPlayer, turnOwner );
        }

        [Test]
        public void ProcessingTurn_MakesMoves() {
            List<IGameMove> moves = new List<IGameMove>();
            moves.Add( GetMoveOfLegalStatus( true ) );
            moves.Add( GetMoveOfLegalStatus( true ) );
            moves.Add( GetMoveOfLegalStatus( true ) );

            PlayerTurn systemUnderTest = new PlayerTurn( Substitute.For<IGamePlayer>(), moves );
            systemUnderTest.Process();

            foreach ( IGameMove move in moves ) {
                move.Received( 1 ).MakeMove();
            }
        }

        private IGameMove GetMoveOfLegalStatus( bool i_status ) {
            IGameMove move = Substitute.For<IGameMove>();
            move.IsLegal( Arg.Any<IGameBoard>() ).Returns( i_status );

            return move;
        }
    }
}