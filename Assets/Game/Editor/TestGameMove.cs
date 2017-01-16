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
    }
}