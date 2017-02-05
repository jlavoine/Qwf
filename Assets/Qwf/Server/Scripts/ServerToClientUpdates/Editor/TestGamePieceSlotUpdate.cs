using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;

namespace Qwf {
    [TestFixture]
    public class TestGamePieceSlotUpdate {
        private GamePieceSlotUpdate mSystemUnderTest;

        [SetUp]
        public void BeforeTest() {
            mSystemUnderTest = new GamePieceSlotUpdate();
        }

        [Test]
        public void WhenCreating_SlotPieceTypeMatchesData() {
            mSystemUnderTest.SlotPieceType = 3;

            Assert.AreEqual( 3, mSystemUnderTest.GetSlotPieceType() );
        }
    }
}
