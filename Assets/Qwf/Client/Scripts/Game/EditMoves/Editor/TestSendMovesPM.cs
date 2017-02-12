using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;

#pragma warning disable 0219

namespace Qwf.Client {
    [TestFixture]
    public class TestSendMovesPM : QwfUnitTest {
        [Test]
        public void WhenCreatingPM_MoveAttemptsListIsEmpty() {
            SendMovesPM systemUnderTest = new SendMovesPM();

            Assert.IsEmpty( systemUnderTest.MoveAttempts );
        }

        [Test]
        public void WhenMovesReset_MoveAttemptsListIsEmpty() {
            SendMovesPM systemUnderTest = new SendMovesPM();
            systemUnderTest.MoveAttempts.Add( Substitute.For<IClientMoveAttempt>() );

            systemUnderTest.OnResetMoves();

            Assert.IsEmpty( systemUnderTest.MoveAttempts );
        }

        [Test]
        public void WhenMoveMade_MoveAttemptsListIsAddedTo() {
            SendMovesPM systemUnderTest = new SendMovesPM();
            systemUnderTest.MoveAttempts = new List<IClientMoveAttempt>();

            IClientMoveAttempt mockMove = Substitute.For<IClientMoveAttempt>();
            systemUnderTest.OnMadeMove( mockMove );

            Assert.AreEqual( 1, systemUnderTest.MoveAttempts.Count );
            Assert.Contains( mockMove, systemUnderTest.MoveAttempts );
        }

        [Test]
        public void WhenProcessingAction_EventWithMovesIsSent() {
            SendMovesPM systemUnderTest = CreateSystemWithMoves( 3 );

            systemUnderTest.ProcessAction();

            MyMessenger.Instance.Received().Send<ClientTurnAttempt>( ClientMessages.SEND_TURN_TO_SERVER, Arg.Is<ClientTurnAttempt>( attempt => attempt.MoveAttempts.Count == 3 ) );
        }

        [Test]
        public void WhenCreatingPM_InteractableProperties_FalseByDefault() {
            SendMovesPM systemUnderTest = new SendMovesPM();

            Assert.AreEqual( 0f, systemUnderTest.ViewModel.GetPropertyValue<float>( PassTurnPM.VISIBLE_PROPERTY ) );
            Assert.IsFalse( systemUnderTest.ViewModel.GetPropertyValue<bool>( MakeMovePM.USE_PROPERTY ) );
        }

        [Test]
        public void WhenCreating_SubscribesToMessages() {
            SendMovesPM systemUnderTest = new SendMovesPM();

            MyMessenger.Instance.Received().AddListener<IClientMoveAttempt>( ClientGameEvents.MADE_MOVE, Arg.Any<Callback<IClientMoveAttempt>>() );
            MyMessenger.Instance.Received().AddListener( ClientGameEvents.RESET_MOVES, Arg.Any<Callback>() );
        }

        [Test]
        public void WhenDisposing_UnsubscribesFromMessages() {
            SendMovesPM systemUnderTest = new SendMovesPM();

            systemUnderTest.Dispose();

            MyMessenger.Instance.Received().RemoveListener<IClientMoveAttempt>( ClientGameEvents.MADE_MOVE, Arg.Any<Callback<IClientMoveAttempt>>() );
            MyMessenger.Instance.Received().RemoveListener( ClientGameEvents.RESET_MOVES, Arg.Any<Callback>() );
        }

        [Test]
        public void WhenMoveIsMade_InteractablePropertiesAreTrue() {
            SendMovesPM systemUnderTest = new SendMovesPM();
            systemUnderTest.ViewModel.SetProperty( SendMovesPM.VISIBLE_PROPERTY, 0f );
            systemUnderTest.ViewModel.SetProperty( SendMovesPM.USE_PROPERTY, 0f );

            systemUnderTest.OnMadeMove( Substitute.For<IClientMoveAttempt>() );

            Assert.AreEqual( 1f, systemUnderTest.ViewModel.GetPropertyValue<float>( SendMovesPM.VISIBLE_PROPERTY ) );
            Assert.IsTrue( systemUnderTest.ViewModel.GetPropertyValue<bool>( MakeMovePM.USE_PROPERTY ) );
        }

        [Test]
        public void WhenMovesReset_InteractablePropertiesAreFalse() {
            SendMovesPM systemUnderTest = new SendMovesPM();
            systemUnderTest.ViewModel.SetProperty( SendMovesPM.VISIBLE_PROPERTY, 1f );
            systemUnderTest.ViewModel.SetProperty( SendMovesPM.USE_PROPERTY, 1f );

            systemUnderTest.OnResetMoves();

            Assert.AreEqual( 0f, systemUnderTest.ViewModel.GetPropertyValue<float>( SendMovesPM.VISIBLE_PROPERTY ) );
            Assert.IsFalse( systemUnderTest.ViewModel.GetPropertyValue<bool>( MakeMovePM.USE_PROPERTY ) );
        }

        [Test]
        public void WhenMoveIsSent_InteractablePropertiesAreFalse() {
            SendMovesPM systemUnderTest = CreateSystemWithMoves( 3 );
            systemUnderTest.ViewModel.SetProperty( SendMovesPM.VISIBLE_PROPERTY, 1f );
            systemUnderTest.ViewModel.SetProperty( SendMovesPM.USE_PROPERTY, 1f );

            systemUnderTest.ProcessAction();

            Assert.AreEqual( 0f, systemUnderTest.ViewModel.GetPropertyValue<float>( SendMovesPM.VISIBLE_PROPERTY ) );
            Assert.IsFalse( systemUnderTest.ViewModel.GetPropertyValue<bool>( MakeMovePM.USE_PROPERTY ) );
        }

        [Test]
        public void WhenMoveIsSent_MovesListIsReset() {
            SendMovesPM systemUnderTest = CreateSystemWithMoves( 3 );

            systemUnderTest.ProcessAction();

            Assert.IsEmpty( systemUnderTest.MoveAttempts );
        }

        private SendMovesPM CreateSystemWithMoves( int i_moves ) {
            SendMovesPM systemUnderTest = new SendMovesPM();
            systemUnderTest.MoveAttempts = new List<IClientMoveAttempt>();
            for ( int i = 0; i < i_moves; ++i ) {
                systemUnderTest.MoveAttempts.Add( new ClientMoveAttempt() );
            }
            
            return systemUnderTest;
        }
    }
}
