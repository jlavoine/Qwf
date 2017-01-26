using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;

namespace Qwf.UnitTests {
    [TestFixture]
    public class TestGameBoard {
        private const int MAX_CURRENT_OBSTACLES = 3;
        private const int STANDARD_OBSTACLE_COUNT = 12;

        [Test]
        public void AfterCreation_CurrentAndRemainingObstacles_MatchOriginalData() {
            List<IGameObstacle> obstacles = GetObstacleList( STANDARD_OBSTACLE_COUNT );
            GameBoard systemUnderTest = CreateSystemUnderTest( obstacles );
            List<IGameObstacle> currentObstacles = systemUnderTest.GetCurrentObstacles();
            List<IGameObstacle> remainingObstacles = systemUnderTest.GetRemainingObstacles();

            Assert.AreEqual( MAX_CURRENT_OBSTACLES, currentObstacles.Count );
            Assert.AreEqual( STANDARD_OBSTACLE_COUNT - MAX_CURRENT_OBSTACLES, remainingObstacles.Count );

            foreach ( IGameObstacle obstacle in currentObstacles ) {
                Assert.Contains( obstacle, obstacles );
            }

            foreach ( IGameObstacle obstacle in remainingObstacles ) {
                Assert.Contains( obstacle, obstacles );
            }
        }

        [Test]
        public void IfNotEnoughRemainingObstaclesExist_CurrentObstaclesAreFilledToCapacity() {
            List<IGameObstacle> obstacles = GetObstacleList( MAX_CURRENT_OBSTACLES - 1 );
            GameBoard systemUnderTest = CreateSystemUnderTest( obstacles );
            List<IGameObstacle> currentObstacles = systemUnderTest.GetCurrentObstacles();
            List<IGameObstacle> remainingObstacles = systemUnderTest.GetRemainingObstacles();

            Assert.AreEqual( MAX_CURRENT_OBSTACLES - 1, currentObstacles.Count );
            Assert.AreEqual( 0, remainingObstacles.Count );
        }

        [Test]
        public void IsObstacleCurrent_ReturnsTrue_WhenObstacleIsCurrent() {
            List<IGameObstacle> obstacles = GetObstacleList( 1 );
            GameBoard systemUnderTest = CreateSystemUnderTest( obstacles );

            IGameObstacle currentObstacle = systemUnderTest.GetCurrentObstacles()[0];
            bool isObstacleCurrent = systemUnderTest.IsObstacleCurrent( currentObstacle );

            Assert.IsTrue( isObstacleCurrent );
        }

        [Test]
        public void IsObstacleCurrent_ReturnsFalse_WhenObstacleIsNotCurrent() {
            List<IGameObstacle> obstacles = GetObstacleList( 1 );
            GameBoard systemUnderTest = CreateSystemUnderTest( obstacles );

            IGameObstacle nonCurrentObstacle = Substitute.For<IGameObstacle>();
            bool isObstacleCurrent = systemUnderTest.IsObstacleCurrent( nonCurrentObstacle );

            Assert.IsFalse( isObstacleCurrent );
        }

        [Test]
        public void WhenUpdatingBoardState_OnlyCompletedObstaclesAreScored() {
            List<IGameObstacle> obstacles = GetObstacleList( 3 );
            obstacles[0].IsComplete().Returns( true );
            obstacles[1].IsComplete().Returns( false );
            obstacles[2].IsComplete().Returns( true );

            GameBoard systemUnderTest = CreateSystemUnderTest( obstacles );
            systemUnderTest.UpdateBoardState( Substitute.For<IScoreKeeper>(), Substitute.For<IGamePlayer>() );

            obstacles[0].Received().Score( Arg.Any<IScoreKeeper>(), Arg.Any<IGamePlayer>() );
            obstacles[1].DidNotReceive().Score( Arg.Any<IScoreKeeper>(), Arg.Any<IGamePlayer>() );
            obstacles[2].Received().Score( Arg.Any<IScoreKeeper>(), Arg.Any<IGamePlayer>() );
        }

        [Test]
        public void WhenUpdatingBoardState_CompletedObstaclesAreRemovedFromCurrentObstacles() {
            List<IGameObstacle> obstacles = GetObstacleList( 3 );
            obstacles[0].IsComplete().Returns( true );
            obstacles[1].IsComplete().Returns( false );
            obstacles[2].IsComplete().Returns( true );

            GameBoard systemUnderTest = CreateSystemUnderTest( obstacles );
            systemUnderTest.UpdateBoardState( Substitute.For<IScoreKeeper>(), Substitute.For<IGamePlayer>() );

            List<IGameObstacle> currentObstacles = systemUnderTest.GetCurrentObstacles();
            bool hasCompleted0 = currentObstacles.Contains( obstacles[0] );
            bool hasCompleted1 = currentObstacles.Contains( obstacles[1] );
            bool hasCompleted2 = currentObstacles.Contains( obstacles[2] );

            Assert.IsFalse( hasCompleted0 );
            Assert.IsTrue( hasCompleted1 );
            Assert.IsFalse( hasCompleted2 );
        }

        private GameBoard CreateSystemUnderTest( List<IGameObstacle> i_obstacles ) {
            return new GameBoard( i_obstacles, MAX_CURRENT_OBSTACLES );
        }

        private List<IGameObstacle> GetObstacleList( int i_obstacleCount ) {
            List<IGameObstacle> obstacleData = new List<IGameObstacle>();

            for ( int i = 0; i < i_obstacleCount; ++i ) {
                obstacleData.Add( Substitute.For<IGameObstacle>() );
            }

            return obstacleData;
        }
    }
}