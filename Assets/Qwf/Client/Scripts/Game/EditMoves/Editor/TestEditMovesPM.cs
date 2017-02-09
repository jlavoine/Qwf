using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;

#pragma warning disable 0219

namespace Qwf.Client {
    [TestFixture]
    public class TestEditMovesPM : QwfUnitTest {
        [Test]
        public void WhenCreating_SubscribesToMessages() {
            EditMovesPM systemUnderTest = new EditMovesPM();

            MyMessenger.Instance.Received().AddListener<ITurnUpdate>( ClientMessages.UPDATE_TURN, Arg.Any<Callback<ITurnUpdate>>() );
        }

        [Test]
        public void WhenDisposing_UnsubscribesFromMessages() {
            EditMovesPM systemUnderTest = new EditMovesPM();

            systemUnderTest.Dispose();

            MyMessenger.Instance.Received().RemoveListener<ITurnUpdate>( ClientMessages.UPDATE_TURN, Arg.Any<Callback<ITurnUpdate>>() );
        }

        [Test]
        public void EditMovesIsNotVisibleByDefault() {
            EditMovesPM systemUnderTest = new EditMovesPM();

            Assert.AreEqual( 0f, systemUnderTest.ViewModel.GetPropertyValue<float>( EditMovesPM.VISIBLE_PROPERTY ) );
        }

        [Test]
        public void OnTurnUpdate_IfActivePlayer_IsVisible() {
            ITurnUpdate mockUpdate = Substitute.For<ITurnUpdate>();
            mockUpdate.IsThisPlayerActive().Returns( true );

            EditMovesPM systemUnderTest = new EditMovesPM();
            systemUnderTest.ViewModel.SetProperty( EditMovesPM.VISIBLE_PROPERTY, 0f );

            systemUnderTest.OnTurnUpdate( mockUpdate );

            Assert.AreEqual( 1f, systemUnderTest.ViewModel.GetPropertyValue<float>( EditMovesPM.VISIBLE_PROPERTY ) );
        }

        [Test]
        public void OnTurnUpdate_IfNotActivePlayer_IsNotVisible() {
            ITurnUpdate mockUpdate = Substitute.For<ITurnUpdate>();
            mockUpdate.IsThisPlayerActive().Returns( false );

            EditMovesPM systemUnderTest = new EditMovesPM();
            systemUnderTest.ViewModel.SetProperty( EditMovesPM.VISIBLE_PROPERTY, 1f );

            systemUnderTest.OnTurnUpdate( mockUpdate );

            Assert.AreEqual( 0f, systemUnderTest.ViewModel.GetPropertyValue<float>( EditMovesPM.VISIBLE_PROPERTY ) );
        }
    }
}
