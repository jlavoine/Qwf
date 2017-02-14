using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;

#pragma warning disable 0219

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
    }
}