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

            MyMessenger.Instance.Received().AddListener<IClientMoveAttempt>( ClientGameEvents.MADE_MOVE, Arg.Any<Callback<IClientMoveAttempt>>() );
            MyMessenger.Instance.Received().AddListener( ClientGameEvents.RESET_MOVES, Arg.Any<Callback>() );
            MyMessenger.Instance.Received().AddListener<ClientTurnAttempt>( ClientMessages.SEND_TURN_TO_SERVER, Arg.Any<Callback<ClientTurnAttempt>>() );
        }

        [Test]
        public void WhenDisposing_UnsubscribesFromMessages() {
            ClientGameManager systemUnderTest = new ClientGameManager();

            systemUnderTest.Dispose();

            MyMessenger.Instance.Received().RemoveListener<IClientMoveAttempt>( ClientGameEvents.MADE_MOVE, Arg.Any<Callback<IClientMoveAttempt>>() );
            MyMessenger.Instance.Received().RemoveListener( ClientGameEvents.RESET_MOVES, Arg.Any<Callback>() );
            MyMessenger.Instance.Received().RemoveListener<ClientTurnAttempt>( ClientMessages.SEND_TURN_TO_SERVER, Arg.Any<Callback<ClientTurnAttempt>>() );
        }

        [Test]
        public void WhenMaxMovesMade_EventIsSent() {
            ClientGameManager systemUnderTest = new ClientGameManager();

            for ( int i = 0; i < ClientGameManager.MAX_MOVES_PER_TURN; ++i ) {
                systemUnderTest.OnMadeMove( Substitute.For<IClientMoveAttempt>() );
            }

            MyMessenger.Instance.Received( 1 ).Send( ClientGameEvents.MAX_MOVES_MADE );
        }

        [Test]
        public void WhenLessThanMaxMovesMade_NoEventSent() {
            ClientGameManager systemUnderTest = new ClientGameManager();

            for ( int i = 0; i < ClientGameManager.MAX_MOVES_PER_TURN-1; ++i ) {
                systemUnderTest.OnMadeMove( Substitute.For<IClientMoveAttempt>() );
            }

            MyMessenger.Instance.DidNotReceive().Send( ClientGameEvents.MAX_MOVES_MADE );
        }

        [Test]
        public void WhenMovesAreReset_MoveCounterIsZero() {
            ClientGameManager systemUnderTest = new ClientGameManager();
            systemUnderTest.MovesMade = 2;

            systemUnderTest.OnResetMoves();

            Assert.AreEqual( 0, systemUnderTest.MovesMade );
        }

        [Test]
        public void AfterTurnIsSent_MoveCounterIsZero() {
            ClientGameManager systemUnderTest = new ClientGameManager();
            systemUnderTest.MovesMade = 2;

            systemUnderTest.OnSentTurn( new ClientTurnAttempt() );

            Assert.AreEqual( 0, systemUnderTest.MovesMade );
        }
    }
}
