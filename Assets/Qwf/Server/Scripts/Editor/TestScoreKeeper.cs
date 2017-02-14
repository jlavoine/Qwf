using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;

namespace Qwf {
    [TestFixture]
    public class TestScoreKeeper {

        [Test]
        public void AddingPlayer_SetsPlayerScoreToZero() {
            IGamePlayer mockPlayer = Substitute.For<IGamePlayer>();

            ScoreKeeper systemUnderTest = new ScoreKeeper();
            systemUnderTest.AddPlayer( mockPlayer );

            int score = systemUnderTest.GetPlayerScore( mockPlayer );
            Assert.AreEqual( 0, score );
        }

        [Test]
        public void AddPointsToPlayer_AddsPointsToScore() {
            IGamePlayer mockPlayer = Substitute.For<IGamePlayer>();

            ScoreKeeper systemUnderTest = new ScoreKeeper();
            systemUnderTest.AddPlayer( mockPlayer );
            systemUnderTest.AddPointsToPlayer( mockPlayer, 100 );

            int score = systemUnderTest.GetPlayerScore( mockPlayer );
            Assert.AreEqual( 100, score );
        }

        [Test]
        public void ScoreKeeper_GetNumPlayers_IsAccurate() {
            ScoreKeeper systemUnderTest = new ScoreKeeper();

            Assert.AreEqual( 0, systemUnderTest.GetNumPlayers() );

            systemUnderTest.AddPlayer( Substitute.For<IGamePlayer>() );
            Assert.AreEqual( 1, systemUnderTest.GetNumPlayers() );
        }

        [Test]
        public void GetWinner_ReturnsPlayerWithHigherScore() {
            ScoreKeeper systemUnderTest = new ScoreKeeper();
            IGamePlayer p1 = Substitute.For<IGamePlayer>();
            p1.Id.Returns( "P1" );

            IGamePlayer p2 = Substitute.For<IGamePlayer>();
            p2.Id.Returns( "P2" );

            systemUnderTest.AddPlayer( p1 );
            systemUnderTest.AddPlayer( p2 );

            systemUnderTest.AddPointsToPlayer( p1, 100 );
            systemUnderTest.AddPointsToPlayer( p2, 200 );

            Assert.AreEqual( "P2", systemUnderTest.GetWinner() );
        }
    }
}