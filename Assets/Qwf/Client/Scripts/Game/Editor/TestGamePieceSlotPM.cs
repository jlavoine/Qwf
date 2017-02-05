using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;

namespace Qwf.Client {
    [TestFixture]
    public class TestGamePieceSlotPM {

        [Test]
        public void WhenCreated_VisibilityDefaultsOn() {
            GamePieceSlotPM systemUnderTest = new GamePieceSlotPM( Substitute.For<IGamePieceSlotUpdate>() );

            Assert.AreEqual( 1f, systemUnderTest.ViewModel.GetPropertyValue<float>( GamePieceSlotPM.VISIBLE_PROPERTY ) );
        }

        [Test]
        public void WhenCreated_SlotPieceTypePropertyMatchesData() {
            IGamePieceSlotUpdate mockUpdate = Substitute.For<IGamePieceSlotUpdate>();
            mockUpdate.GetSlotPieceType().Returns( 3 );

            GamePieceSlotPM systemUnderTest = new GamePieceSlotPM( mockUpdate );

            Assert.AreEqual( 3, systemUnderTest.ViewModel.GetPropertyValue<int>( GamePieceSlotPM.SLOT_PIECE_TYPE_PROPERTY ) );
        }
    }
}