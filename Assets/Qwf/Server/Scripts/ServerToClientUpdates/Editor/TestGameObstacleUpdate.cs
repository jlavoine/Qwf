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
    }
}