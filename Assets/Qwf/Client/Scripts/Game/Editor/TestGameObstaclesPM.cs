using NUnit.Framework;
using NSubstitute;
using MyLibrary;

#pragma warning disable 0414

namespace Qwf.Client {
    [TestFixture]
    public class TestGameObstaclesPM {
        [SetUp]
        public void BeforeTest() {
            MyMessenger.Instance = Substitute.For<IMessageService>();
        }

        [TearDown]
        public void AfterTest() {
            MyMessenger.Instance = null;
        }

        [Test]
        public void WhenCreating_IndividualPMsCountMatchesCreationData() {
            IGameObstaclesUpdate mockUpdate = Substitute.For<IGameObstaclesUpdate>();
            mockUpdate.GetObstaclesCount().Returns( 3 );

            GameObstaclesPM systemUnderTest = new GameObstaclesPM( mockUpdate );

            Assert.AreEqual( 3, systemUnderTest.ObstaclePMs.Count );
        }

        [Test]
        public void OnCreating_SubscribeToUpdateFromServer() {
            IGameObstaclesUpdate mockUpdate = Substitute.For<IGameObstaclesUpdate>();
            GameObstaclesPM systemUnderTest = new GameObstaclesPM( mockUpdate );

            MyMessenger.Instance.Received().AddListener<IGameObstaclesUpdate>( ClientMessages.UPDATE_OBSTACLES, Arg.Any<Callback<IGameObstaclesUpdate>>() );
        }

        [Test]
        public void OnDisposing_UnsubscribeToUpdateFromServer() {
            IGameObstaclesUpdate mockUpdate = Substitute.For<IGameObstaclesUpdate>();
            GameObstaclesPM systemUnderTest = new GameObstaclesPM( mockUpdate );

            systemUnderTest.Dispose();

            MyMessenger.Instance.Received().RemoveListener<IGameObstaclesUpdate>( ClientMessages.UPDATE_OBSTACLES, Arg.Any<Callback<IGameObstaclesUpdate>>() );
        }
    }
}
