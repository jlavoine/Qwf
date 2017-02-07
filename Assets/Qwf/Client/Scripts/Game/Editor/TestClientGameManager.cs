using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;

namespace Qwf.Client {
    [TestFixture]
    public class TestClientGameManager : QwfUnitTest {

        [Test]
        public void WhenCreating_SubscribesToMessages() {
            ClientGameManager systemUnderTest = new ClientGameManager();

            MyMessenger.Instance.Received().AddListener( ClientGameEvents.MADE_MOVE, Arg.Any<Callback>() );
        }

        [Test]
        public void WhenDisposing_UnsubscribesFromMessages() {
            ClientGameManager systemUnderTest = new ClientGameManager();

            systemUnderTest.Dispose();

            MyMessenger.Instance.Received().RemoveListener( ClientGameEvents.MADE_MOVE, Arg.Any<Callback>() );
        }
    }
}
