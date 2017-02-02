using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;

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
            MatchScorePM systemUnderTest = new MatchScorePM();

            Assert.AreEqual( "0", systemUnderTest.ViewModel.GetPropertyValue<string>( MatchScorePM.PLAYER_SCORE_PROPERTY ) );
        }

        [Test]
        public void WhenCreatingPM_OpponentScorePropertySetToZero() {
            MatchScorePM systemUnderTest = new MatchScorePM();

            Assert.AreEqual( "0", systemUnderTest.ViewModel.GetPropertyValue<string>( MatchScorePM.OPPONENT_SCORE_PROPERTY ) );
        }

        [Test]
        public void WhenCreating_SystemSubscribesToUpdate() {
            MatchScorePM systemUnderTest = new MatchScorePM();

            MyMessenger.Instance.Received().AddListener<MatchScoreUpdateData>( ClientMessages.UPDATE_SCORE, Arg.Any<Callback<MatchScoreUpdateData>>() );
        }

        [Test]
        public void WhenDisposing_SystemUnsubscribesToUpdate() {
            MatchScorePM systemUnderTest = new MatchScorePM();

            systemUnderTest.Dispose();

            MyMessenger.Instance.Received().RemoveListener<MatchScoreUpdateData>( ClientMessages.UPDATE_SCORE, Arg.Any<Callback<MatchScoreUpdateData>>() );
        }
    }
}
