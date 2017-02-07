using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;

#pragma warning disable 0219

namespace Qwf.Client {
    [TestFixture]
    public class TestPassTurnPM : QwfUnitTest {
        [Test]
        public void WhenCreatingPM_IsVisibleProperty_TrueByDefault() {
            PassTurnPM systemUnderTest = new PassTurnPM();

            Assert.AreEqual( 1f, systemUnderTest.ViewModel.GetPropertyValue<float>( PassTurnPM.VISIBLE_PROPERTY ) );
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
        public void WhenMoveIsMade_IsVisibleIsFalse() {
            PassTurnPM systemUnderTest = new PassTurnPM();
            systemUnderTest.ViewModel.SetProperty( PassTurnPM.VISIBLE_PROPERTY, 1f );

            systemUnderTest.OnMadeMove();

            Assert.AreEqual( 0f, systemUnderTest.ViewModel.GetPropertyValue<float>( PassTurnPM.VISIBLE_PROPERTY ) );
        }

        [Test]
        public void WhenMovesReset_IsVisibleIsTrue() {
            PassTurnPM systemUnderTest = new PassTurnPM();
            systemUnderTest.ViewModel.SetProperty( PassTurnPM.VISIBLE_PROPERTY, 0f );

            systemUnderTest.OnResetMoves();

            Assert.AreEqual( 1f, systemUnderTest.ViewModel.GetPropertyValue<float>( PassTurnPM.VISIBLE_PROPERTY ) );
        }
    }
}