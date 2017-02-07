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
    }
}