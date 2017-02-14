using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;

#pragma warning disable 0219

namespace Qwf.Client {
    [TestFixture]
    public class TestGameOverUpdate : QwfUnitTest {

        [Test]
        public void DidClientWin_ReturnsTrue_WhenClientWon() {
            BackendManager.Instance.GetPlayerId().Returns( "Me" );
            GameOverUpdate systemUnderTest = new GameOverUpdate();
            systemUnderTest.Winner = "Me";

            Assert.IsTrue( systemUnderTest.DidClientWin() );
        }

        [Test]
        public void DidClientWin_ReturnsFalse_WhenClientLost() {
            BackendManager.Instance.GetPlayerId().Returns( "Me" );
            GameOverUpdate systemUnderTest = new GameOverUpdate();
            systemUnderTest.Winner = "Them";

            Assert.IsFalse( systemUnderTest.DidClientWin() );
        }
    }
}
