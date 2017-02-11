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
        public void WhenCreating_VariablesAreExpected() {
            GamePieceData blankPiece = new GamePieceData();

            mSystemUnderTest.SlotPieceType = 3;
            mSystemUnderTest.PieceInSlot = blankPiece;
            mSystemUnderTest.Index = 1;
            mSystemUnderTest.ObstacleIndex = 5;

            Assert.AreEqual( 3, mSystemUnderTest.GetSlotPieceType() );
            Assert.AreEqual( blankPiece, mSystemUnderTest.GetPieceInSlot() );
            Assert.AreEqual( 1, mSystemUnderTest.GetIndex() );
            Assert.AreEqual( 5, mSystemUnderTest.GetObstacleIndex() );
        } 
    }
}
