using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;

namespace Qwf.UnitTests {
    [TestFixture]
    public class TestGameBoard {
        private const int MAX_CURRENT_OBSTACLES = 3;
        private const int STANDARD_OBSTACLE_COUNT = 12;

        private GameBoard CreateSystemUnderTest( List<GameObstacleData> i_obstacleData ) {
            GameBoardData data = new GameBoardData() { MaxCurrentObstacles = 3, ObstacleData = i_obstacleData };
            return new GameBoard( data );
        }

        private List<GameObstacleData> CreateObstacleData( int i_obstacleCount ) {
            List<GameObstacleData> obstacleData = new List<GameObstacleData>();

            for ( int i = 0; i < i_obstacleCount; ++i ) {
                obstacleData.Add( new GameObstacleData() { SlotsData = new List<GamePieceSlotData>() } );
            }

            return obstacleData;
        }

        [Test]
        public void AfterCreation_CurrentAndRemainingObstacles_MatchOriginalData() {
            List<GameObstacleData> obstacleData = CreateObstacleData( STANDARD_OBSTACLE_COUNT );
            GameBoard systemUnderTest = CreateSystemUnderTest( obstacleData );
            List<IGameObstacle> currentObstacles = systemUnderTest.GetCurrentObstacles();
            List<IGameObstacle> remainingObstacles = systemUnderTest.GetRemainingObstacles();

            Assert.AreEqual( MAX_CURRENT_OBSTACLES, currentObstacles.Count );
            Assert.AreEqual( STANDARD_OBSTACLE_COUNT - MAX_CURRENT_OBSTACLES, remainingObstacles.Count );

            foreach ( IGameObstacle obstacle in currentObstacles ) {
                Assert.Contains( obstacle.GetData(), obstacleData );
            }

            foreach ( IGameObstacle obstacle in remainingObstacles ) {
                Assert.Contains( obstacle.GetData(), obstacleData );
            }
        }
    }
}