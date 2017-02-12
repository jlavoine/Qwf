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
         
        }
    }
}