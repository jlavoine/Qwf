using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;

#pragma warning disable 0219

namespace Qwf.Client {
    [TestFixture]
    public class TestPassTurnPM : QwfUnitTest {
        [Test]
        public void WhenCreatingPM_InteractableProperties_TrueByDefault() {
            PassTurnPM systemUnderTest = new PassTurnPM();

            Assert.AreEqual( 1f, systemUnderTest.ViewModel.GetPropertyValue<float>( PassTurnPM.VISIBLE_PROPERTY ) );
            Assert.IsTrue( systemUnderTest.ViewModel.GetPropertyValue<bool>( MakeMovePM.USE_PROPERTY ) );
        }

        [Test]
        public void WhenCreating_SubscribesToMessages() {
            PassTurnPM systemUnderTest = new PassTurnPM();

            MyMessenger.Instance.Received().AddListener( ClientGameEvents.MADE_MOVE, Arg.Any<Callback>() );
            MyMessenger.Instance.Received().AddListener( ClientGameEvents.RESET_MOVES, Arg.Any<Callback>() );
        }

        [Test]
        public void WhenDisposing_UnsubscribesFromMessages() {
            PassTurnPM systemUnderTest = new PassTurnPM();

            systemUnderTest.Dispose();

            MyMessenger.Instance.Received().RemoveListener( ClientGameEvents.MADE_MOVE, Arg.Any<Callback>() );
            MyMessenger.Instance.Received().RemoveListener( ClientGameEvents.RESET_MOVES, Arg.Any<Callback>() );
        }

        [Test]
        public void WhenMoveIsMade_InteractablePropertiesAreFalse() {
            PassTurnPM systemUnderTest = new PassTurnPM();
            systemUnderTest.ViewModel.SetProperty( PassTurnPM.VISIBLE_PROPERTY, 1f );

            systemUnderTest.OnMadeMove();

            Assert.AreEqual( 0f, systemUnderTest.ViewModel.GetPropertyValue<float>( PassTurnPM.VISIBLE_PROPERTY ) );
            Assert.IsFalse( systemUnderTest.ViewModel.GetPropertyValue<bool>( MakeMovePM.USE_PROPERTY ) );
        }

        [Test]
        public void WhenMovesReset_InteractablePropertiesAreTrue() {
            PassTurnPM systemUnderTest = new PassTurnPM();
            systemUnderTest.ViewModel.SetProperty( PassTurnPM.VISIBLE_PROPERTY, 0f );
            systemUnderTest.ViewModel.SetProperty( SendMovesPM.USE_PROPERTY, 0f );

            systemUnderTest.OnResetMoves();

            Assert.AreEqual( 1f, systemUnderTest.ViewModel.GetPropertyValue<float>( PassTurnPM.VISIBLE_PROPERTY ) );
            Assert.IsTrue( systemUnderTest.ViewModel.GetPropertyValue<bool>( MakeMovePM.USE_PROPERTY ) );
        }

        [Test]
        public void WhenPassingTurn_EmptyMoveAttemptIsSent() {
            BackendManager.Instance.GetPlayerId().Returns( "Me" );
            PassTurnPM systemUnderTest = new PassTurnPM();

            systemUnderTest.ProcessAction();

            MyMessenger.Instance.Received().Send<ClientTurnAttempt>( ClientMessages.SEND_TURN_TO_SERVER, Arg.Is<ClientTurnAttempt>(attempt => attempt.PlayerId == "Me" && attempt.MoveAttempts == null ) );
        }
    }
}