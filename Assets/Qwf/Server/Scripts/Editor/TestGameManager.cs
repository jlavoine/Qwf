using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;

namespace Qwf {
    [TestFixture]
    public class TestGameManager {
        [Test]
        public void IfPlayerTurnIsNotValid_TurnIsNotProcessed() {
            IPlayerTurn mockTurn = Substitute.For<IPlayerTurn>();
            mockTurn.IsValid( Arg.Any<IGameBoard>() ).Returns( false );

            GameManager systemUnderTest = new GameManager( Substitute.For<IGameBoard>(), Substitute.For<IScoreKeeper>() );
            systemUnderTest.TryPlayerTurn( mockTurn );

            mockTurn.DidNotReceive().Process();
        }

        [Test]
        public void IfPlayerTurnIsValid_TurnIsProcessed() {
            IPlayerTurn mockTurn = Substitute.For<IPlayerTurn>();
            mockTurn.IsValid( Arg.Any<IGameBoard>() ).Returns( true );

            GameManager systemUnderTest = new GameManager( Substitute.For<IGameBoard>(), Substitute.For<IScoreKeeper>() );
            systemUnderTest.TryPlayerTurn( mockTurn );

            mockTurn.Received().Process();
        }

        [Test]
        public void AfterTurnIsProcessed_CheckForGameOver() {
            IGameBoard mockBoard = Substitute.For<IGameBoard>();
            GameManager systemUnderTest = new GameManager( mockBoard, Substitute.For<IScoreKeeper>() );

            TakeValidTurn( systemUnderTest );

            mockBoard.Received().IsGameOver();
        }

        [Test]
        public void AfterTurnIsProcessed_PlayersHandIsFilled() {
            IGamePlayer mockPlayer = Substitute.For<IGamePlayer>();
            IPlayerTurn mockTurn = Substitute.For<IPlayerTurn>();
            mockTurn.IsValid( Arg.Any<IGameBoard>() ).Returns( true );
            mockTurn.GetPlayer().Returns( mockPlayer );

            GameManager systemUnderTest = new GameManager( Substitute.For<IGameBoard>(), Substitute.For<IScoreKeeper>() );
            systemUnderTest.TryPlayerTurn( mockTurn );

            mockPlayer.Received().DrawToFillHand();
        }

        [Test]
        public void AfterTurnIsProcessed_GameBoardIsUpdated() {
            IGameBoard mockBoard = Substitute.For<IGameBoard>();
            GameManager systemUnderTest = new GameManager( mockBoard, Substitute.For<IScoreKeeper>() );

            TakeValidTurn( systemUnderTest );

            mockBoard.Received().UpdateBoardState( Arg.Any<IScoreKeeper>(), Arg.Any<IGamePlayer>() );
        }

        [Test]
        public void AddingPlayer_AddsToPlayerList() {
            GameManager systemUnderTest = new GameManager();
            IGamePlayer mockPlayer = Substitute.For<IGamePlayer>();
            mockPlayer.Id.Returns( "Joe" );

            systemUnderTest.AddPlayer( mockPlayer, "Joe" );

            IGamePlayer addedPlayer = systemUnderTest.GetPlayerFromId( "Joe" );
            Assert.AreEqual( "Joe", addedPlayer.Id );
            Assert.AreEqual( mockPlayer, addedPlayer );
        }

        [Test]
        public void GettingPlayerThatDoesNotExist_ReturnsNull() {
            GameManager systemUnderTest = new GameManager();

            IGamePlayer noPlayer = systemUnderTest.GetPlayerFromId( "Nobody" );

            Assert.IsNull( noPlayer );
        }

        private void TakeValidTurn( GameManager i_manager ) {
            IPlayerTurn mockTurn = Substitute.For<IPlayerTurn>();
            mockTurn.IsValid( Arg.Any<IGameBoard>() ).Returns( true );

            i_manager.TryPlayerTurn( mockTurn );
        }
    }
}
