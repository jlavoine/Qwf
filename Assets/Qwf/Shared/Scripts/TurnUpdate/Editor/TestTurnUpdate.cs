using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;

namespace Qwf.Client {
    [TestFixture]
    public class TestTurnUpdate : QwfUnitTest {
        [Test]
        public void GetActivePlayer_ReturnsExpectedValue() {
            TurnUpdate update = new TurnUpdate();
            update.ActivePlayer = "Me";

            Assert.AreEqual( "Me", update.GetActivePlayer() );
        }
    }
}
