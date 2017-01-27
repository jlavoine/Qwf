using NUnit.Framework;
using System.Collections.Generic;
using NSubstitute;

namespace Qwf.UnitTests {
    [TestFixture]
    public class TestGameObstacle {
        public const int OBSTACLE_SCORE_VALUE = 5;

        [Test]
        public void CorrectNumberOfSlots_AreCreatedForObstacle() {
            List<IGamePieceSlot> slots = new List<IGamePieceSlot>();
            slots.Add( Substitute.For<IGamePieceSlot>() );
            slots.Add( Substitute.For<IGamePieceSlot>() );
            slots.Add( Substitute.For<IGamePieceSlot>() );

            GameObstacle systemUnderTest = new GameObstacle( slots, OBSTACLE_SCORE_VALUE );

            Assert.AreEqual( slots.Count, systemUnderTest.GetSlots().Count );
        }

        [Test]
        public void IfObstacleDoesNotHaveSlot_PieceCannotBePlacedInSlot() {
            List<IGamePieceSlot> slots = new List<IGamePieceSlot>();
            slots.Add( Substitute.For<IGamePieceSlot>() );

            GameObstacle systemUnderTest = new GameObstacle( slots, OBSTACLE_SCORE_VALUE );

            IGamePieceSlot differentSlot = Substitute.For<IGamePieceSlot>();
            bool canPlace = systemUnderTest.CanPieceBePlacedIntoSlot( Substitute.For<IGamePiece>(), differentSlot );

            Assert.IsFalse( canPlace );
        }

        [Test]
        public void IfObstacleHasSlot_ButPieceCannotBeInSlot_PieceCannotBePlacedInSlot() {
            List<IGamePieceSlot> mockSlots = new List<IGamePieceSlot>();
            mockSlots.Add( Substitute.For<IGamePieceSlot>() );
            GameObstacle systemUnderTest = new GameObstacle( mockSlots, OBSTACLE_SCORE_VALUE );

            mockSlots[0].CanPlacePieceIntoSlot( Arg.Any<IGamePiece>() ).Returns( false );
            bool canPlace = systemUnderTest.CanPieceBePlacedIntoSlot( Substitute.For<IGamePiece>(), mockSlots[0] );

            Assert.IsFalse( canPlace );
        }

        [Test]
        public void IfObstacleHasSlot_AndPieceCanBeInSlot_PieceCanBePlacedInSlot() {
            List<IGamePieceSlot> mockSlots = new List<IGamePieceSlot>();
            mockSlots.Add( Substitute.For<IGamePieceSlot>() );
            GameObstacle systemUnderTest = new GameObstacle( mockSlots, OBSTACLE_SCORE_VALUE );
            
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

            GameObstacle systemUnderTest = new GameObstacle( mockSlots, OBSTACLE_SCORE_VALUE );

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

            GameObstacle systemUnderTest = new GameObstacle( mockSlots, OBSTACLE_SCORE_VALUE );

            Assert.IsFalse( systemUnderTest.IsComplete() );
        }

        [Test]
        public void ScoringObstacle_ScoresAllSlots() {
            List<IGamePieceSlot> mockSlots = new List<IGamePieceSlot>();
            mockSlots.Add( Substitute.For<IGamePieceSlot>() );
            mockSlots.Add( Substitute.For<IGamePieceSlot>() );
            mockSlots.Add( Substitute.For<IGamePieceSlot>() );

            GameObstacle systemUnderTest = new GameObstacle( mockSlots, OBSTACLE_SCORE_VALUE );
            systemUnderTest.Score( Substitute.For<IScoreKeeper>(), Substitute.For<IGamePlayer>() );

            foreach ( IGamePieceSlot slot in mockSlots ) {
                slot.Received().Score( Arg.Any<IScoreKeeper>() );
            }
        }

        [Test]
        public void ScoringObstacles_GivesPlayerPoints() {
            IScoreKeeper mockScoreKeeper = Substitute.For<IScoreKeeper>();
            IGamePlayer mockPlayer = Substitute.For<IGamePlayer>();
            List<IGamePieceSlot> mockSlots = new List<IGamePieceSlot>();
            mockSlots.Add( Substitute.For<IGamePieceSlot>() );

            GameObstacle systemUnderTest = new GameObstacle( mockSlots, OBSTACLE_SCORE_VALUE );
            systemUnderTest.Score( mockScoreKeeper, mockPlayer );

            mockScoreKeeper.Received().AddPointsToPlayer( mockPlayer, OBSTACLE_SCORE_VALUE );
        }
    }
}