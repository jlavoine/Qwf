using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;

namespace Qwf.UnitTests {
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
            IPlayerTurn mockTurn = Substitute.For<IPlayerTurn>();
            mockTurn.IsValid( Arg.Any<IGameBoard>() ).Returns( true );            

            GameManager systemUnderTest = new GameManager( mockBoard, Substitute.For<IScoreKeeper>() );
            systemUnderTest.TryPlayerTurn( mockTurn );

            mockBoard.Received().UpdateBoardState( Arg.Any<IScoreKeeper>() );
        }
    }
}
