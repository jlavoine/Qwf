using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;

#pragma warning disable 0414

namespace Qwf.Client {
    [TestFixture]
    public class TestTurnUpdate : QwfUnitTest {
        // originally, a TurnUpdate held the id of the active player.
        // but I thought it was unwise to be passing PlayFab ids around, so I changed it
        // it to just return a bool
        /*[Test]
        public void GetActivePlayer_ReturnsExpectedValue() {
            TurnUpdate systemUnderTest = new TurnUpdate();
            systemUnderTest.ActivePlayer = "Me";

            Assert.AreEqual( "Me", systemUnderTest.GetActivePlayer() );
        }

        static object[] ActivePlayerTests = {
            new object[] { "Me", "Me", true },
            new object[] { "Me", "Them", false }
        };

        [Test, TestCaseSource("ActivePlayerTests")]
        public void IsThisPlayerActive_ReturnsExpectedValue( string i_thisPlayer, string i_activePlayer, bool i_expected ) {
            BackendManager.Instance.GetPlayerId().Returns( i_thisPlayer );
            TurnUpdate systemUnderTest = new TurnUpdate();
            systemUnderTest.ActivePlayer = i_activePlayer;

            Assert.AreEqual( i_expected, systemUnderTest.IsThisPlayerActive() );
        }*/
    }
}
