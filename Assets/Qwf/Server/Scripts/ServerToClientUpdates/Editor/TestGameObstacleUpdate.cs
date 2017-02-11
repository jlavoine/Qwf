using NUnit.Framework;
using System.Collections.Generic;

namespace Qwf {
    [TestFixture]
    public class TestGameObstacleUpdate {

        private GameObstacleUpdate mSystemUnderTest;

        [SetUp]
        public void BeforeTest() {
            mSystemUnderTest = new GameObstacleUpdate();
        }

        [Test]
        public void GetIdCall_EqualsCreatedWithId() {
            mSystemUnderTest.Id = "Goblin";

            Assert.AreEqual( "Goblin", mSystemUnderTest.GetId() );
        }

        [Test]
        public void GetImageName_ReturnsExpected() {
            mSystemUnderTest.Id = "Goblin";

            Assert.AreEqual( GameObstacleUpdate.IMAGE_PREFIX + "Goblin", mSystemUnderTest.GetImageKey() );
        }

        [Test]
        public void GetFinalBlowValue_EqualsCreatedWithValue() {
            mSystemUnderTest.FinalBlowValue = 11;

            Assert.AreEqual( 11, mSystemUnderTest.GetFinalBlowValue() );
        }

        [Test]
        public void GetIndex_EqualsCreatedWithValue() {
            mSystemUnderTest.Index = 1;

            Assert.AreEqual( 1, mSystemUnderTest.GetIndex() );
        }         

        [Test]
        public void GetSlotCount_ReturnsExpectedCount() {
            List<GamePieceSlotUpdate> slotUpdates = new List<GamePieceSlotUpdate>();
            slotUpdates.Add( new GamePieceSlotUpdate() );
            slotUpdates.Add( new GamePieceSlotUpdate() );
            slotUpdates.Add( new GamePieceSlotUpdate() );
            slotUpdates.Add( new GamePieceSlotUpdate() );
            mSystemUnderTest.PieceSlots = slotUpdates;

            Assert.AreEqual( slotUpdates.Count, mSystemUnderTest.GetSlotCount() );
        }

        [Test]
        public void GetSlotUpdate_ReturnsExpectedValue() {
            List<GamePieceSlotUpdate> slotUpdates = new List<GamePieceSlotUpdate>();
            GamePieceSlotUpdate one = new GamePieceSlotUpdate();
            GamePieceSlotUpdate two = new GamePieceSlotUpdate();
            slotUpdates.Add( one );
            slotUpdates.Add( two );

            mSystemUnderTest.PieceSlots = slotUpdates;

            Assert.AreEqual( two, mSystemUnderTest.GetSlotUpdate( 1 ) );
        }
    }
}