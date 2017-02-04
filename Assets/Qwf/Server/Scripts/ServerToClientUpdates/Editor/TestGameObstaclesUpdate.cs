using NUnit.Framework;
using System.Collections.Generic;
using NSubstitute;

namespace Qwf {
    public class TestGameObstaclesUpdate {

        [Test]
        public void GetUpdateCall_ReturnsProperUpdateObject() {
            GameObstaclesUpdate systemUnderTest = new GameObstaclesUpdate();

            List<GameObstacleUpdate> obs = new List<GameObstacleUpdate>();
            GameObstacleUpdate obs1 = new GameObstacleUpdate();
            GameObstacleUpdate obs2 = new GameObstacleUpdate();
            obs.Add( obs1 );
            obs.Add( obs2 );
            systemUnderTest.Obstacles = obs;

            Assert.AreEqual( obs1, systemUnderTest.GetUpdate( 0 ) );
        }

        [Test]
        public void GetCountCall_ReturnsCorrectCount() {
            GameObstaclesUpdate systemUnderTest = new GameObstaclesUpdate();

            List<GameObstacleUpdate> obs = new List<GameObstacleUpdate>();
            obs.Add( new GameObstacleUpdate() );
            obs.Add( new GameObstacleUpdate() );
            obs.Add( new GameObstacleUpdate() );
            systemUnderTest.Obstacles = obs;

            Assert.AreEqual( 3, systemUnderTest.GetObstaclesCount() );
        }
    }
}
