using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;

#pragma warning disable 0219

namespace Qwf.Client {
    [TestFixture]
    public class TestMatchMakerPM : QwfUnitTest {

        [Test]
        public void WhenCreating_SubscribesToMessages() {
            MatchMakerPM systemUnderTest = new MatchMakerPM();

            MyMessenger.Instance.Received().AddListener( ClientMessages.GAME_READY, Arg.Any<Callback>() );
        }

        [Test]
        public void WhenDisposing_UnsubscribesFromMessages() {
            MatchMakerPM systemUnderTest = new MatchMakerPM();

            systemUnderTest.Dispose();

            MyMessenger.Instance.Received().RemoveListener( ClientMessages.GAME_READY, Arg.Any<Callback>() );
        }

        [Test]
        public void ByDefault_StatusPropertyIsSearching() {
            StringTableManager.Instance.Get( MatchMakerPM.SEARCHING_KEY ).Returns( "Searching Text" );
            MatchMakerPM systemUnderTest = new MatchMakerPM();

            Assert.AreEqual( "Searching Text", systemUnderTest.ViewModel.GetPropertyValue<string>( MatchMakerPM.STATUS_PROPERTY ) );
        }

        [Test]
        public void ByDefault_VisiblePropertyIsTrue() {
            MatchMakerPM systemUnderTest = new MatchMakerPM();

            Assert.IsTrue( systemUnderTest.ViewModel.GetPropertyValue<bool>( MatchMakerPM.VISIBLE_PROPERTY ) );
        }

        [Test]
        public void OnGameReadyReceived_VisiblePropertyIsFalse() {
            MatchMakerPM systemUnderTest = new MatchMakerPM();

            systemUnderTest.OnGameReady();

            Assert.IsFalse( systemUnderTest.ViewModel.GetPropertyValue<bool>( MatchMakerPM.VISIBLE_PROPERTY ) );
        }
    }
}
