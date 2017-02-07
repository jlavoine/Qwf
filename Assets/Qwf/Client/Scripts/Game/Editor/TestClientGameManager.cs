using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;

#pragma warning disable 0219

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

        [Test]
        public void WhenMaxMovesMade_EventIsSent() {
            ClientGameManager systemUnderTest = new ClientGameManager();

            for ( int i = 0; i < ClientGameManager.MAX_MOVES_PER_TURN; ++i ) {
                systemUnderTest.OnMadeMove();
            }

            MyMessenger.Instance.Received( 1 ).Send( ClientGameEvents.MAX_MOVES_MADE );
        }

        [Test]
        public void WhenLessThanMaxMovesMade_NoEventSent() {
            ClientGameManager systemUnderTest = new ClientGameManager();

            for ( int i = 0; i < ClientGameManager.MAX_MOVES_PER_TURN-1; ++i ) {
                systemUnderTest.OnMadeMove();
            }

            MyMessenger.Instance.DidNotReceive().Send( ClientGameEvents.MAX_MOVES_MADE );
        }
    }
}
