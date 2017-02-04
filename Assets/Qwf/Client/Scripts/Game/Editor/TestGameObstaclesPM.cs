using NUnit.Framework;
using NSubstitute;
using UnityEngine;

namespace Qwf.Client {
    [TestFixture]
    public class TestGameObstaclesPM {

        [Test]
        public void WhenCreating_IndividualPMsCountMatchesCreationData() {
            IGameObstaclesUpdate mockUpdate = Substitute.For<IGameObstaclesUpdate>();
            mockUpdate.GetObstaclesCount().Returns( 3 );

            GameObstaclesPM systemUnderTest = new GameObstaclesPM( mockUpdate );

            Assert.AreEqual( 3, systemUnderTest.ObstaclePMs.Count );
        }
    }
}
