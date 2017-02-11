﻿using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;

#pragma warning disable 0219

namespace Qwf.Client {
    [TestFixture]
    public class TestResetMovesPM : QwfUnitTest {

        [Test]
        public void ByDefault_InteractablePropertiesAreFalse() {
            ResetMovesPM systemUnderTest = new ResetMovesPM();

            AssertIsVisibleProperty( systemUnderTest, false );
            Assert.AreEqual( false, systemUnderTest.ViewModel.GetPropertyValue<bool>( ResetMovesPM.USE_PROPERTY ) );
        }

        [Test]
        public void WhenCreating_SubscribesToMessages() {
            ResetMovesPM systemUnderTest = new ResetMovesPM();

            MyMessenger.Instance.Received().AddListener( ClientGameEvents.MADE_MOVE, Arg.Any<Callback>() );
            MyMessenger.Instance.Received().AddListener<IGameObstaclesUpdate>( ClientMessages.UPDATE_OBSTACLES, Arg.Any<Callback<IGameObstaclesUpdate>>() );
        }

        [Test]
        public void WhenDisposing_UnsubscribesFromMessages() {
            ResetMovesPM systemUnderTest = new ResetMovesPM();

            systemUnderTest.Dispose();

            MyMessenger.Instance.Received().RemoveListener( ClientGameEvents.MADE_MOVE, Arg.Any<Callback>() );
            MyMessenger.Instance.Received().RemoveListener<IGameObstaclesUpdate>( ClientMessages.UPDATE_OBSTACLES, Arg.Any<Callback<IGameObstaclesUpdate>>() );
        }

        [Test]
        public void AfterMoveIsMade_InteractablePropertiesAreTrue() {
            ResetMovesPM systemUnderTest = new ResetMovesPM();

            systemUnderTest.OnMadeMove();

            AssertIsVisibleProperty( systemUnderTest, true );
            Assert.AreEqual( true, systemUnderTest.ViewModel.GetPropertyValue<bool>( ResetMovesPM.USE_PROPERTY ) );
        }

        [Test]
        public void WhenMovesAreReset_EventIsSent() {
            ResetMovesPM systemUnderTest = new ResetMovesPM();

            systemUnderTest.ResetMoves();

            MyMessenger.Instance.Received().Send( ClientGameEvents.RESET_MOVES );
        }

        [Test]
        public void WhenMovesAreReset_CachedObstaclesUpdateIsSentOut() {
            ResetMovesPM systemUnderTest = new ResetMovesPM();

            systemUnderTest.ResetMoves();

            MyMessenger.Instance.Received().Send<IGameObstaclesUpdate>( ClientMessages.UPDATE_OBSTACLES, Arg.Any<IGameObstaclesUpdate>() );
        }

        [Test]
        public void WhenMovesAreReset_InteractablePropertiesAsExpected() {
            ResetMovesPM systemUnderTest = new ResetMovesPM();
            systemUnderTest.ViewModel.SetProperty( ResetMovesPM.VISIBLE_PROPERTY, 1f );

            systemUnderTest.ResetMoves();

            AssertIsVisibleProperty( systemUnderTest, false );
            Assert.AreEqual( false, systemUnderTest.ViewModel.GetPropertyValue<bool>( ResetMovesPM.USE_PROPERTY ) );
        }

        [Test]
        public void OnReceivingObstaclesUpdate_UpdateIsCached() {
            GameObstaclesUpdate mockUpdate = new GameObstaclesUpdate();
            ResetMovesPM systemUnderTest = new ResetMovesPM();

            systemUnderTest.OnUpdateObstacles( mockUpdate );

            Assert.AreEqual( mockUpdate, systemUnderTest.CachedUpdate );
        }

        private void AssertIsVisibleProperty( ResetMovesPM i_systemUnderTest, bool i_visible ) {
            float fAlpha = i_visible ? 1f : 0f;
            Assert.AreEqual( fAlpha, i_systemUnderTest.ViewModel.GetPropertyValue<float>( ResetMovesPM.VISIBLE_PROPERTY ) );
        }
    }
}
