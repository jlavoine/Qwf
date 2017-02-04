using NUnit.Framework;
using System.Collections.Generic;

#pragma warning disable 0414

namespace Qwf {
    [TestFixture]
    public class TestMatchScoreUpdateData {
        public const string PLAYER_1_ID = "P1";
        public const string PLAYER_2_ID = "P2";
        public const string NOT_PLAYING_ID = "P3";

        public const int PLAYER_1_SCORE = 100;
        public const int PLAYER_2_SCORE = 5;

        private MatchScoreUpdateData mSystemUnderTest;

        [SetUp]
        public void BeforeTest() {
            mSystemUnderTest = new MatchScoreUpdateData();
            mSystemUnderTest.Scores = new Dictionary<string, int>();
            mSystemUnderTest.Scores.Add( PLAYER_1_ID, PLAYER_1_SCORE );
            mSystemUnderTest.Scores.Add( PLAYER_2_ID, PLAYER_2_SCORE );
        }

        static object[] PlayerScoreTests = {
            new object[] { PLAYER_1_ID, PLAYER_1_SCORE },
            new object[] { PLAYER_2_ID, PLAYER_2_SCORE },
            new object[] { NOT_PLAYING_ID, 0 }
        };

        [Test, TestCaseSource( "PlayerScoreTests" )]
        public void MatchScoreUpdates_ReturnsCorrectPlayerScore( string i_playerId, int i_expectedResult ) {
            Assert.AreEqual( i_expectedResult, mSystemUnderTest.GetScoreForPlayer( i_playerId ) );
        }

        static object[] OpponentScoreTests = {
            new object[] { PLAYER_1_ID, PLAYER_2_SCORE },
            new object[] { PLAYER_2_ID, PLAYER_1_SCORE }
        };

        [Test, TestCaseSource( "OpponentScoreTests" )]
        public void MatchScoreUpdates_ReturnsCorrectOpponentScore( string i_playerId, int i_expectedResult ) {
            Assert.AreEqual( i_expectedResult, mSystemUnderTest.GetScoreForOpponent( i_playerId ) );
        }
    }
}
