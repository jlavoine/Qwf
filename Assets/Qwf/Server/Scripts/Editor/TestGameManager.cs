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
        public void AfterTurnIsProcessed_ActiveAndInactivePlayersSwitch() {
            GameManager systemUnderTest = new GameManager( Substitute.For<IGameBoard>(), Substitute.For<IScoreKeeper>() );
            IGamePlayer p1 = Substitute.For<IGamePlayer>();
            p1.Id.Returns( "P1" );
            IGamePlayer p2 = Substitute.For<IGamePlayer>();
            p2.Id.Returns( "P2" );
            systemUnderTest.ActivePlayer = p1;
            systemUnderTest.InactivePlayer = p2;

            TakeValidTurn( systemUnderTest );

            Assert.AreEqual( p1, systemUnderTest.InactivePlayer );
            Assert.AreEqual( p2, systemUnderTest.ActivePlayer );
        }

        static object[] GameManagerReadyTests = {
            new object[] { Substitute.For<IGameBoard>(), Substitute.For<IScoreKeeper>(), Substitute.For<IGamePlayer>(), Substitute.For<IGamePlayer>(), true },
            new object[] { null, Substitute.For<IScoreKeeper>(), Substitute.For<IGamePlayer>(), Substitute.For<IGamePlayer>(), false },
            new object[] { Substitute.For<IGameBoard>(), Substitute.For<IScoreKeeper>(), Substitute.For<IGamePlayer>(), null, false },
            new object[] { Substitute.For<IGameBoard>(), Substitute.For<IScoreKeeper>(), null, Substitute.For<IGamePlayer>(), false },
            new object[] { Substitute.For<IGameBoard>(), null, Substitute.For<IGamePlayer>(), Substitute.For<IGamePlayer>(), false },

        };

        [Test, TestCaseSource("GameManagerReadyTests")]
        public void GameManagerIsReady_WhenExpected( IGameBoard i_board, IScoreKeeper i_scoreKeeper, IGamePlayer i_player1, IGamePlayer i_player2, bool i_expected ) {
            GameManager systemUnderTest = new GameManager( i_board, i_scoreKeeper );

            if ( i_player1 != null ) {
                i_player1.Id.Returns( "P1" );
                systemUnderTest.AddPlayer( i_player1 );
            }

            if ( i_player2 != null ) {
                i_player2.Id.Returns( "P2" );
                systemUnderTest.AddPlayer( i_player2 );
            }
            
            Assert.AreEqual( i_expected, systemUnderTest.IsReady() );
        }

        [Test]
        public void AddingPlayer_AddsToPlayerList() {
            GameManager systemUnderTest = new GameManager();
            IGamePlayer mockPlayer = Substitute.For<IGamePlayer>();
            mockPlayer.Id.Returns( "Joe" );

            systemUnderTest.AddPlayer( mockPlayer );

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
