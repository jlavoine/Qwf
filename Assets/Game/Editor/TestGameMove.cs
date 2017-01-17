using NUnit.Framework;
using NSubstitute;

namespace Qwf.UnitTests {
    [TestFixture]
    public class TestGameMove {

        [Test]
        public void TargetVariablesAreSameAsUsedToCreate() {
            IGamePiece targetPiece = Substitute.For<IGamePiece>();
            IGamePieceSlot targetSlot = Substitute.For<IGamePieceSlot>();
            IGameObstacle targetObstacle = Substitute.For<IGameObstacle>();
            GameMove systemUnderTest = new GameMove( targetPiece, targetObstacle, targetSlot );

            Assert.AreEqual( targetPiece, systemUnderTest.GetTargetPiece() );
            Assert.AreEqual( targetObstacle, systemUnderTest.GetTargetObstacle() );
            Assert.AreEqual( targetSlot, systemUnderTest.GetTargetSlot() );
        }

        [Test]
        public void IfPlayerDoesNotHavePiece_ButAllOtherPartsLegal_MoveIsNotLegal() {
            IGamePiece targetPiece = Substitute.For<IGamePiece>();
            targetPiece.IsCurrentlyHeld().Returns( false );

            IGamePieceSlot targetSlot = Substitute.For<IGamePieceSlot>();

            IGameObstacle targetObstacle = Substitute.For<IGameObstacle>();
            targetObstacle.CanPieceBePlacedIntoSlot( Arg.Any<IGamePiece>(), Arg.Any<IGamePieceSlot>() ).Returns( true );

            IGameBoard mockBoard = Substitute.For<IGameBoard>();
            mockBoard.IsObstacleCurrent( Arg.Any<IGameObstacle>() ).Returns( true );

            GameMove systemUnderTest = new GameMove( targetPiece, targetObstacle, targetSlot );

            Assert.IsFalse( systemUnderTest.IsLegal( mockBoard ) );
        }

        [Test]
        public void IfObstacleSlotTestFails_ButAllOtherPartsLegal_MoveIsNotLegal() {
            IGamePiece targetPiece = Substitute.For<IGamePiece>();
            targetPiece.IsCurrentlyHeld().Returns( true );

            IGamePieceSlot targetSlot = Substitute.For<IGamePieceSlot>();

            IGameObstacle targetObstacle = Substitute.For<IGameObstacle>();
            targetObstacle.CanPieceBePlacedIntoSlot( Arg.Any<IGamePiece>(), Arg.Any<IGamePieceSlot>() ).Returns( false );

            IGameBoard mockBoard = Substitute.For<IGameBoard>();
            mockBoard.IsObstacleCurrent( Arg.Any<IGameObstacle>() ).Returns( true );

            GameMove systemUnderTest = new GameMove( targetPiece, targetObstacle, targetSlot );

            Assert.IsFalse( systemUnderTest.IsLegal( mockBoard ) );
        }

        [Test]
        public void IfPlayerHasPiece_AndObstacleIsCurrent_AndTargetSlotTakesPiece_MoveIsLegal() {
            IGamePiece targetPiece = Substitute.For<IGamePiece>();
            targetPiece.IsCurrentlyHeld().Returns( true );

            IGamePieceSlot targetSlot = Substitute.For<IGamePieceSlot>();

            IGameObstacle targetObstacle = Substitute.For<IGameObstacle>();
            targetObstacle.CanPieceBePlacedIntoSlot( Arg.Any<IGamePiece>(), Arg.Any<IGamePieceSlot>() ).Returns( true );

            IGameBoard mockBoard = Substitute.For<IGameBoard>();
            mockBoard.IsObstacleCurrent( Arg.Any<IGameObstacle>() ).Returns( true );

            GameMove systemUnderTest = new GameMove( targetPiece, targetObstacle, targetSlot );

            Assert.IsTrue( systemUnderTest.IsLegal( mockBoard ) );
        }

        [Test]
        public void WhenMoveIsMade_TargetPieceNowInTargetSlot() {
            IGamePiece targetPiece = Substitute.For<IGamePiece>();
            IGamePieceSlot targetSlot = Substitute.For<IGamePieceSlot>();
            IGameObstacle targetObstacle = Substitute.For<IGameObstacle>();
            GameMove systemUnderTest = new GameMove( targetPiece, targetObstacle, targetSlot );
            
            systemUnderTest.MakeMove();
            
            targetSlot.Received( 1 ).PlacePieceIntoSlot( targetPiece );
        }
    }
}