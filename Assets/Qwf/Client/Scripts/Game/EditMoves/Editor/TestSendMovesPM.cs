using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;

#pragma warning disable 0219

namespace Qwf.Client {
    [TestFixture]
    public class TestSendMovesPM : QwfUnitTest {
        [Test]
        public void WhenCreatingPM_IsVisibleProperty_FalseByDefault() {
            SendMovesPM systemUnderTest = new SendMovesPM();

            Assert.AreEqual( 0f, systemUnderTest.ViewModel.GetPropertyValue<float>( PassTurnPM.VISIBLE_PROPERTY ) );
        }

        [Test]
        public void WhenCreating_SubscribesToMessages() {
            SendMovesPM systemUnderTest = new SendMovesPM();

            MyMessenger.Instance.Received().AddListener( ClientGameEvents.MADE_MOVE, Arg.Any<Callback>() );
            MyMessenger.Instance.Received().AddListener( ClientGameEvents.RESET_MOVES, Arg.Any<Callback>() );
        }

        [Test]
        public void WhenDisposing_UnsubscribesFromMessages() {
            SendMovesPM systemUnderTest = new SendMovesPM();

            systemUnderTest.Dispose();

            MyMessenger.Instance.Received().RemoveListener( ClientGameEvents.MADE_MOVE, Arg.Any<Callback>() );
            MyMessenger.Instance.Received().RemoveListener( ClientGameEvents.RESET_MOVES, Arg.Any<Callback>() );
        }

        [Test]
        public void WhenMoveIsMade_IsVisibleIsTrue() {
            SendMovesPM systemUnderTest = new SendMovesPM();
            systemUnderTest.ViewModel.SetProperty( SendMovesPM.VISIBLE_PROPERTY, 0f );

            systemUnderTest.OnMadeMove();

            Assert.AreEqual( 1f, systemUnderTest.ViewModel.GetPropertyValue<float>( SendMovesPM.VISIBLE_PROPERTY ) );
        }

        [Test]
        public void WhenMovesReset_IsVisibleIsFalse() {
            SendMovesPM systemUnderTest = new SendMovesPM();
            systemUnderTest.ViewModel.SetProperty( SendMovesPM.VISIBLE_PROPERTY, 1f );

            systemUnderTest.OnResetMoves();

            Assert.AreEqual( 0f, systemUnderTest.ViewModel.GetPropertyValue<float>( SendMovesPM.VISIBLE_PROPERTY ) );
        }
    }
}
