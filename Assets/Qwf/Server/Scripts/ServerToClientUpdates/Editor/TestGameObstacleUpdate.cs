using NUnit.Framework;
using System.Collections.Generic;

namespace Qwf {
    [TestFixture]
    public class TestGameObstacleUpdate {
        [Test]
        public void GetIdCall_EqualsCreatedWithId() {
            GameObstacleUpdate systemUnderTest = new GameObstacleUpdate();
            systemUnderTest.Id = "Goblin";

            Assert.AreEqual( "Goblin", systemUnderTest.GetId() );
        }

        [Test]
        public void GetImageName_ReturnsExpected() {
            GameObstacleUpdate systemUnderTest = new GameObstacleUpdate();
            systemUnderTest.Id = "Goblin";

            Assert.AreEqual( GameObstacleUpdate.IMAGE_PREFIX + "Goblin", systemUnderTest.GetImageKey() );
        }
    }
}