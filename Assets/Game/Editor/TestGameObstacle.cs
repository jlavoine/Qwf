using NUnit.Framework;
using System.Collections.Generic;
using NSubstitute;

namespace Qwf.UnitTests {
    [TestFixture]
    public class TestGameObstacle {

        [Test]
        public void CorrectNumberOfSlots_AreCreatedForObstacle() {
            List<GamePieceSlotData> slotData = new List<GamePieceSlotData>();
            slotData.Add( new GamePieceSlotData() );
            slotData.Add( new GamePieceSlotData() );
            slotData.Add( new GamePieceSlotData() );

            GameObstacleData data = new GameObstacleData() { SlotsData = slotData };
            GameObstacle systemUnderTest = new GameObstacle( data );

            Assert.AreEqual( slotData.Count, systemUnderTest.GetSlots().Count );
        }

        [Test]
        public void IfObstacleDoesNotHaveSlot_PieceCannotBePlacedInSlot() {
            List<GamePieceSlotData> slotData = new List<GamePieceSlotData>();
            slotData.Add( new GamePieceSlotData() );

            GameObstacleData data = new GameObstacleData() { SlotsData = slotData };
            GameObstacle systemUnderTest = new GameObstacle( data );

            IGamePieceSlot differentSlot = Substitute.For<IGamePieceSlot>();
            bool canPlace = systemUnderTest.CanPieceBePlacedIntoSlot( Substitute.For<IGamePiece>(), differentSlot );

            Assert.IsFalse( canPlace );
        }

        [Test]
        public void IfObstacleHasSlot_ButPieceCannotBeInSlot_PieceCannotBePlacedInSlot() {
            List<IGamePieceSlot> mockSlots = new List<IGamePieceSlot>();
            mockSlots.Add( Substitute.For<IGamePieceSlot>() );
            GameObstacle systemUnderTest = new GameObstacle( mockSlots );

            mockSlots[0].CanPlacePieceIntoSlot( Arg.Any<IGamePiece>() ).Returns( false );
            bool canPlace = systemUnderTest.CanPieceBePlacedIntoSlot( Substitute.For<IGamePiece>(), mockSlots[0] );

            Assert.IsFalse( canPlace );
        }

        [Test]
        public void IfObstacleHasSlot_AndPieceCanBeInSlot_PieceCanBePlacedInSlot() {
            List<IGamePieceSlot> mockSlots = new List<IGamePieceSlot>();
            mockSlots.Add( Substitute.For<IGamePieceSlot>() );
            GameObstacle systemUnderTest = new GameObstacle( mockSlots );
            
            mockSlots[0].CanPlacePieceIntoSlot( Arg.Any<IGamePiece>() ).Returns( true );
            bool canPlace = systemUnderTest.CanPieceBePlacedIntoSlot( Substitute.For<IGamePiece>(), mockSlots[0] );

            Assert.IsTrue( canPlace );
        }

        [Test]
        public void WhenAllSlotsAreFull_ObstacleIsComplete() {
            IGamePieceSlot fullSlot = Substitute.For<IGamePieceSlot>();
            fullSlot.IsEmpty().Returns( false );
            List<IGamePieceSlot> mockSlots = new List<IGamePieceSlot>();
            mockSlots.Add( fullSlot );

            GameObstacle systemUnderTest = new GameObstacle( mockSlots );

            Assert.IsTrue( systemUnderTest.IsComplete() );
        }

        [Test]
        public void WhenAnySlotIsEmpty_ObstacleIsNotComplete() {
            IGamePieceSlot fullSlot = Substitute.For<IGamePieceSlot>();
            fullSlot.IsEmpty().Returns( false );

            IGamePieceSlot emptySlot = Substitute.For<IGamePieceSlot>();
            emptySlot.IsEmpty().Returns( true );

            List<IGamePieceSlot> mockSlots = new List<IGamePieceSlot>();
            mockSlots.Add( fullSlot );
            mockSlots.Add( emptySlot );

            GameObstacle systemUnderTest = new GameObstacle( mockSlots );

            Assert.IsFalse( systemUnderTest.IsComplete() );
        }
    }
}