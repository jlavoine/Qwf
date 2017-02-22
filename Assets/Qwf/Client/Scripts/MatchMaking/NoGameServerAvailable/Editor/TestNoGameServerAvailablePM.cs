using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;

#pragma warning disable 0219

namespace Qwf.Client {
    [TestFixture]
    public class TestNoGameServerAvailablePM : QwfUnitTest {

        [Test]
        public void WhenCreating_SubscribesToMessages() {
            NoGameSeverAvailablePM systemUnderTest = new NoGameSeverAvailablePM();

            MyMessenger.Instance.Received().AddListener( ClientMessages.NO_SERVER_AVAILABLE, Arg.Any<Callback>() );
        }

        [Test]
        public void WhenDisposing_UnsubscribesFromMessages() {
            NoGameSeverAvailablePM systemUnderTest = new NoGameSeverAvailablePM();

            systemUnderTest.Dispose();

            MyMessenger.Instance.Received().RemoveListener( ClientMessages.NO_SERVER_AVAILABLE, Arg.Any<Callback>() );
        }

        [Test]
        public void WhenCreating_PropertiesSetAsExpected() {
            StringTableManager.Instance.Get( NoGameSeverAvailablePM.NO_GAME_SERVER_STRING_KEY ).Returns( "SomeText" );
            NoGameSeverAvailablePM systemUnderTest = new NoGameSeverAvailablePM();

            Assert.IsFalse( systemUnderTest.ViewModel.GetPropertyValue<bool>( NoGameSeverAvailablePM.VISIBLE_PROPERTY ) );
            Assert.AreEqual( "SomeText", systemUnderTest.ViewModel.GetPropertyValue<string>( NoGameSeverAvailablePM.TEXT_PROPERTY ) );
        }

        [Test]
        public void WhenReceivingNoServerAvailableMessage_IsVisiblePropertyIsTrue() {
            NoGameSeverAvailablePM systemUnderTest = new NoGameSeverAvailablePM();

            systemUnderTest.OnNoServerAvailable();

            Assert.IsTrue( systemUnderTest.ViewModel.GetPropertyValue<bool>( NoGameSeverAvailablePM.VISIBLE_PROPERTY ) );
        }
    }
}
