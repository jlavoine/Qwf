﻿using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;

#pragma warning disable 0219
#pragma warning disable 0414

namespace Qwf.Client {
    [TestFixture]
    public class TestGameOverPM : QwfUnitTest {
        [Test]
        public void WhenCreating_SubscribesToMessages() {
            GameOverPM systemUnderTest = new GameOverPM();

            MyMessenger.Instance.Received().AddListener<IGameOverUpdate>( ClientMessages.GAME_OVER_UPDATE, Arg.Any<Callback<IGameOverUpdate>>() );            
        }

        [Test]
        public void WhenDisposing_UnsubscribesFromMessages() {
            GameOverPM systemUnderTest = new GameOverPM();

            systemUnderTest.Dispose();

            MyMessenger.Instance.Received().RemoveListener<IGameOverUpdate>( ClientMessages.GAME_OVER_UPDATE, Arg.Any<Callback<IGameOverUpdate>>() );
        }

        [Test]
        public void IsVisibleProperty_FalseByDefault() {
            GameOverPM systemUnderTest = new GameOverPM();

            bool isVisible = systemUnderTest.ViewModel.GetPropertyValue<bool>( GameOverPM.VISIBLE_PROPERTY );
            Assert.IsFalse( isVisible );
        }

        [Test]
        public void AfterGameOverMessage_IsVisibleTrue() {
            GameOverPM systemUnderTest = new GameOverPM();

            systemUnderTest.OnGameOver( Substitute.For<IGameOverUpdate>() );

            bool isVisible = systemUnderTest.ViewModel.GetPropertyValue<bool>( GameOverPM.VISIBLE_PROPERTY );
            Assert.IsTrue( isVisible );
        }

        static object[] BodyTextTests = {
            new object[] { "You lose", GameOverPM.LOST_GAME_KEY, false },
            new object[] { "You win", GameOverPM.WON_GAME_KEY, true }
        };

        [Test, TestCaseSource("BodyTextTests")]
        public void AfterGameOverMessage_BodyTextPropertyAsExpected( string i_expectedMessage, string i_key, bool i_won ) {
            StringTableManager.Instance.Get( i_key ).Returns( i_expectedMessage );
            IGameOverUpdate mockUpdate = Substitute.For<IGameOverUpdate>();
            mockUpdate.DidClientWin().Returns( i_won );

            GameOverPM systemUnderTest = new GameOverPM();
            systemUnderTest.OnGameOver( mockUpdate );

            Assert.AreEqual( i_expectedMessage, systemUnderTest.ViewModel.GetPropertyValue<string>( GameOverPM.BODY_TEXT_PROPERTY ) );
        }
    }
}