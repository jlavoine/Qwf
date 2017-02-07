using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;

#pragma warning disable 0219

namespace Qwf.Client {
    [TestFixture]
    public class TestPlayerHandGamePiecePM : QwfUnitTest {
        [Test]
        public void WhenCreating_SubscribesToMessages() {
            PlayerHandGamePiecePM systemUnderTest = new PlayerHandGamePiecePM( Substitute.For<IGamePieceData>(), "" );

            MyMessenger.Instance.Received().AddListener( ClientGameEvents.MAX_MOVES_MADE, Arg.Any<Callback>() );
        }

        [Test]
        public void WhenDisposing_UnsubscribesFromMessages() {
            PlayerHandGamePiecePM systemUnderTest = new PlayerHandGamePiecePM( Substitute.For<IGamePieceData>(), "" );

            systemUnderTest.Dispose();

            MyMessenger.Instance.Received().RemoveListener( ClientGameEvents.MAX_MOVES_MADE, Arg.Any<Callback>() );
        }

        [Test]
        public void WhenCreating_DefaultPropertiesSet() {
            PlayerHandGamePiecePM systemUnderTest = new PlayerHandGamePiecePM( Substitute.For<IGamePieceData>(), "" );

            Assert.IsTrue( systemUnderTest.ViewModel.GetPropertyValue<bool>( PlayerHandGamePiecePM.CAN_MOVE_PROPERTY ) );
            Assert.AreEqual( 1f, systemUnderTest.ViewModel.GetPropertyValue<float>( PlayerHandGamePiecePM.VISIBLE_PROPERTY ) );
        }

        [Test]
        public void WhenMaxMovesMade_CanMovePropertyIsFalse() {
            PlayerHandGamePiecePM systemUnderTest = new PlayerHandGamePiecePM( Substitute.For<IGamePieceData>(), "" );

            systemUnderTest.OnMaxMovesMade();

            Assert.IsFalse( systemUnderTest.ViewModel.GetPropertyValue<bool>( PlayerHandGamePiecePM.CAN_MOVE_PROPERTY ) );
        }

        [Test]
        public void WhenPlayingPiece_MoveMadeEventIsSent() {
            PlayerHandGamePiecePM systemUnderTest = new PlayerHandGamePiecePM( Substitute.For<IGamePieceData>(), "" );

            systemUnderTest.Play();

            MyMessenger.Instance.Received( 1 ).Send( ClientGameEvents.MADE_MOVE );
        }

        [Test]
        public void WhenPlayingPiece_CanSeePieceProperty_IsFalse() {
            PlayerHandGamePiecePM systemUnderTest = new PlayerHandGamePiecePM( Substitute.For<IGamePieceData>(), "" );

            systemUnderTest.Play();

            Assert.AreEqual( 0f, systemUnderTest.ViewModel.GetPropertyValue<float>( PlayerHandGamePiecePM.VISIBLE_PROPERTY ) );
        }

        [Test]
        public void WhenAttemptingToPlay_CanMovePropertyIsFalse() {
            PlayerHandGamePiecePM systemUnderTest = new PlayerHandGamePiecePM( Substitute.For<IGamePieceData>(), "" );

            systemUnderTest.AttemptingToPlay();

            Assert.IsFalse( systemUnderTest.ViewModel.GetPropertyValue<bool>( PlayerHandGamePiecePM.CAN_MOVE_PROPERTY ) );
        }

        [Test]
        public void AfterInvalidMoveAttempt_CanMovePropertyIsTrue() {
            PlayerHandGamePiecePM systemUnderTest = new PlayerHandGamePiecePM( Substitute.For<IGamePieceData>(), "" );
            systemUnderTest.ViewModel.SetProperty( PlayerHandGamePiecePM.CAN_MOVE_PROPERTY, false );

            systemUnderTest.InvalidPlayAttempt();

            Assert.IsTrue( systemUnderTest.ViewModel.GetPropertyValue<bool>( PlayerHandGamePiecePM.CAN_MOVE_PROPERTY ) );
        }
    }
}