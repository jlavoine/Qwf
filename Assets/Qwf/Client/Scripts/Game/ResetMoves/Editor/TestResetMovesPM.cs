using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;

#pragma warning disable 0219

namespace Qwf.Client {
    [TestFixture]
    public class TestResetMovesPM : QwfUnitTest {

        [Test]
        public void ByDefault_IsVisibleProperty_IsFalse() {
            ResetMovesPM systemUnderTest = new ResetMovesPM();

            Assert.AreEqual( 0f, systemUnderTest.ViewModel.GetPropertyValue<float>( ResetMovesPM.IS_VISIBLE_PROPERTY ) );
        }

        [Test]
        public void WhenCreating_SubscribesToMessages() {
            ResetMovesPM systemUnderTest = new ResetMovesPM();

            MyMessenger.Instance.Received().AddListener( ClientGameEvents.MADE_MOVE, Arg.Any<Callback>() );
        }

        [Test]
        public void WhenDisposing_UnsubscribesFromMessages() {
            ResetMovesPM systemUnderTest = new ResetMovesPM();

            systemUnderTest.Dispose();

            MyMessenger.Instance.Received().RemoveListener( ClientGameEvents.MADE_MOVE, Arg.Any<Callback>() );

        }

        [Test]
        public void AfterMoveIsMade_IsVisiblePropertyIsTrue() {
            ResetMovesPM systemUnderTest = new ResetMovesPM();

            systemUnderTest.OnMadeMove();

            Assert.AreEqual( 1f, systemUnderTest.ViewModel.GetPropertyValue<float>( ResetMovesPM.IS_VISIBLE_PROPERTY ) );
        }
    }
}
