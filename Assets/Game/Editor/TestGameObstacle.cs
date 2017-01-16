using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;

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
    }
}