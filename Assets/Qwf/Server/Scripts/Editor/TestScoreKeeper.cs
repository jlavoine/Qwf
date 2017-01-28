using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;

namespace Qwf.UnitTests {
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
    }
}