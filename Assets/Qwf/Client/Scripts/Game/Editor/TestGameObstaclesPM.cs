using NUnit.Framework;
using NSubstitute;
using MyLibrary;

#pragma warning disable 0414
#pragma warning disable 0219

namespace Qwf.Client {
    [TestFixture]
    public class TestGameObstaclesPM : QwfUnitTest {

        [Test]
        public void WhenCreating_IndividualPMsCountMatchesCreationData() {
            GameObstaclesPM systemUnderTest = new GameObstaclesPM();

            Assert.AreEqual( 3, systemUnderTest.ObstaclePMs.Count );
        }

        [Test]
        public void OnCreating_SubscribeToUpdateFromServer() {
            GameObstaclesUpdate mockUpdate = Substitute.For<GameObstaclesUpdate>();
            GameObstaclesPM systemUnderTest = new GameObstaclesPM();

            MyMessenger.Instance.Received().AddListener<IGameObstaclesUpdate>( ClientMessages.UPDATE_OBSTACLES, Arg.Any<Callback<IGameObstaclesUpdate>>() );
        }

        [Test]
        public void OnDisposing_UnsubscribeToUpdateFromServer() {
            IGameObstaclesUpdate mockUpdate = Substitute.For<GameObstaclesUpdate>();
            GameObstaclesPM systemUnderTest = new GameObstaclesPM();

            systemUnderTest.Dispose();

            MyMessenger.Instance.Received().RemoveListener<IGameObstaclesUpdate>( ClientMessages.UPDATE_OBSTACLES, Arg.Any<Callback<IGameObstaclesUpdate>>() );
        }
    }
}
