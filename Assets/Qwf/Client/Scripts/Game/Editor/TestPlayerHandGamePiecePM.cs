﻿using NUnit.Framework;
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
            MyMessenger.Instance.Received().AddListener( ClientGameEvents.RESET_MOVES, Arg.Any<Callback>() );
            MyMessenger.Instance.Received().AddListener<ITurnUpdate>( ClientMessages.UPDATE_TURN, Arg.Any<Callback<ITurnUpdate>>() );
        }

        [Test]
        public void WhenDisposing_UnsubscribesFromMessages() {
            PlayerHandGamePiecePM systemUnderTest = new PlayerHandGamePiecePM( Substitute.For<IGamePieceData>(), "" );

            systemUnderTest.Dispose();

            MyMessenger.Instance.Received().RemoveListener( ClientGameEvents.MAX_MOVES_MADE, Arg.Any<Callback>() );
            MyMessenger.Instance.Received().RemoveListener( ClientGameEvents.RESET_MOVES, Arg.Any<Callback>() );
            MyMessenger.Instance.Received().RemoveListener<ITurnUpdate>( ClientMessages.UPDATE_TURN, Arg.Any<Callback<ITurnUpdate>>() );
        }

        [Test]
        public void WhenCreating_DefaultPropertiesSet() {
            PlayerHandGamePiecePM systemUnderTest = new PlayerHandGamePiecePM( Substitute.For<IGamePieceData>(), "" );

            Assert.IsFalse( systemUnderTest.ViewModel.GetPropertyValue<bool>( PlayerHandGamePiecePM.CAN_MOVE_PROPERTY ) );
            Assert.AreEqual( 1f, systemUnderTest.ViewModel.GetPropertyValue<float>( PlayerHandGamePiecePM.VISIBLE_PROPERTY ) );
        }

        [Test]
        public void WhenMaxMovesMade_CanMovePropertyIsFalse() {
            PlayerHandGamePiecePM systemUnderTest = new PlayerHandGamePiecePM( Substitute.For<IGamePieceData>(), "" );

            systemUnderTest.OnMaxMovesMade();

            Assert.IsFalse( systemUnderTest.ViewModel.GetPropertyValue<bool>( PlayerHandGamePiecePM.CAN_MOVE_PROPERTY ) );
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

        [Test]
        public void AfterMovesAreReset_IfPieceWasPlayed_IsNowVisible() {
            PlayerHandGamePiecePM systemUnderTest = new PlayerHandGamePiecePM( Substitute.For<IGamePieceData>(), "" );
            systemUnderTest.ViewModel.SetProperty( PlayerHandGamePiecePM.VISIBLE_PROPERTY, 0f );

            systemUnderTest.OnMovesReset();

            Assert.AreEqual( 1f, systemUnderTest.ViewModel.GetPropertyValue<float>( PlayerHandGamePiecePM.VISIBLE_PROPERTY ) );
        }

        [Test]
        public void AfterMovesAreReset_IfPieceWasPlayed_CanNowMoveAgain() {
            PlayerHandGamePiecePM systemUnderTest = new PlayerHandGamePiecePM( Substitute.For<IGamePieceData>(), "" );
            systemUnderTest.ViewModel.SetProperty( PlayerHandGamePiecePM.CAN_MOVE_PROPERTY, false );

            systemUnderTest.OnMovesReset();

            Assert.IsTrue( systemUnderTest.ViewModel.GetPropertyValue<bool>( PlayerHandGamePiecePM.CAN_MOVE_PROPERTY ) );
        }

        [Test]
        public void AfterMovesAreReset_IfPieceDataWasNull_StillNotVisible() {
            PlayerHandGamePiecePM systemUnderTest = new PlayerHandGamePiecePM( null, "" );

            systemUnderTest.OnMovesReset();

            Assert.AreEqual( 0f, systemUnderTest.ViewModel.GetPropertyValue<float>( PlayerHandGamePiecePM.VISIBLE_PROPERTY ) );
        }

        [Test]
        public void WhenPlayersTurn_CanMovePropertyIsTrue() {
            PlayerHandGamePiecePM systemUnderTest = new PlayerHandGamePiecePM( null, "" );
            ITurnUpdate mockUpdate = Substitute.For<ITurnUpdate>();
            mockUpdate.IsThisPlayerActive().Returns( true );
            systemUnderTest.ViewModel.SetProperty( PlayerHandGamePiecePM.CAN_MOVE_PROPERTY, false );

            systemUnderTest.OnTurnUpdate( mockUpdate );

            Assert.IsTrue( systemUnderTest.ViewModel.GetPropertyValue<bool>( PlayerHandGamePiecePM.CAN_MOVE_PROPERTY ) );
        }

        [Test]
        public void WhenNotPlayersTurn_CanMovePropertyIsFalse() {
            PlayerHandGamePiecePM systemUnderTest = new PlayerHandGamePiecePM( null, "" );
            ITurnUpdate mockUpdate = Substitute.For<ITurnUpdate>();
            mockUpdate.IsThisPlayerActive().Returns( false );
            systemUnderTest.ViewModel.SetProperty( PlayerHandGamePiecePM.CAN_MOVE_PROPERTY, true );

            systemUnderTest.OnTurnUpdate( mockUpdate );

            Assert.IsFalse( systemUnderTest.ViewModel.GetPropertyValue<bool>( PlayerHandGamePiecePM.CAN_MOVE_PROPERTY ) );
        }

        [Test]
        public void SettingIndexOnPiece_ReturnsCorrectIndex() {
            PlayerHandGamePiecePM systemUnderTest = new PlayerHandGamePiecePM( null, "" );
            systemUnderTest.SetIndex( 3 );

            Assert.AreEqual( 3, systemUnderTest.Index );
        }
    }
}