using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;

#pragma warning disable 0219

namespace Qwf.Client {
    [TestFixture]
    public class TestMatchScorePM {

        [SetUp]
        public void BeforeTest() {
            MyMessenger.Instance = Substitute.For<IMessageService>();
        }

        [TearDown]
        public void AfterTest() {
            MyMessenger.Instance = null;
        }        

        [Test]
        public void WhenCreatingPM_PlayerScorePropertySetToZero() {
            MatchScorePM systemUnderTest = new MatchScorePM( string.Empty );

            Assert.AreEqual( "0", systemUnderTest.ViewModel.GetPropertyValue<string>( MatchScorePM.PLAYER_SCORE_PROPERTY ) );
        }

        [Test]
        public void WhenCreatingPM_OpponentScorePropertySetToZero() {
            MatchScorePM systemUnderTest = new MatchScorePM( string.Empty );

            Assert.AreEqual( "0", systemUnderTest.ViewModel.GetPropertyValue<string>( MatchScorePM.OPPONENT_SCORE_PROPERTY ) );
        }

        [Test]
        public void WhenCreating_SystemSubscribesToUpdate() {
            MatchScorePM systemUnderTest = new MatchScorePM( string.Empty );

            MyMessenger.Instance.Received().AddListener<IMatchScoreUpdateData>( ClientMessages.UPDATE_SCORE, Arg.Any<Callback<IMatchScoreUpdateData>>() );
        }

        [Test]
        public void WhenDisposing_SystemUnsubscribesToUpdate() {
            MatchScorePM systemUnderTest = new MatchScorePM( string.Empty );

            systemUnderTest.Dispose();

            MyMessenger.Instance.Received().RemoveListener<IMatchScoreUpdateData>( ClientMessages.UPDATE_SCORE, Arg.Any<Callback<IMatchScoreUpdateData>>() );
        }

        [Test]
        public void WhenUpdating_PlayerScoreGetsSetToValueInUpdate() {
            IMatchScoreUpdateData mockUpdate = Substitute.For<IMatchScoreUpdateData>();
            mockUpdate.GetScoreForPlayer( Arg.Any<string>() ).Returns( 100 );
            MatchScorePM systemUnderTest = new MatchScorePM( string.Empty );

            systemUnderTest.OnUpdateScore( mockUpdate );

            Assert.AreEqual( "100", systemUnderTest.ViewModel.GetPropertyValue<string>( MatchScorePM.PLAYER_SCORE_PROPERTY ) );
        }

        [Test]
        public void WhenUpdating_OpponentScoreGetsSetToValueInUpdate() {
            IMatchScoreUpdateData mockUpdate = Substitute.For<IMatchScoreUpdateData>();
            mockUpdate.GetScoreForOpponent( Arg.Any<string>() ).Returns( 22 );
            MatchScorePM systemUnderTest = new MatchScorePM( string.Empty );

            systemUnderTest.OnUpdateScore( mockUpdate );

            Assert.AreEqual( "22", systemUnderTest.ViewModel.GetPropertyValue<string>( MatchScorePM.OPPONENT_SCORE_PROPERTY ) );
        }
    }
}
