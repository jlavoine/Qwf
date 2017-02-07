using NUnit.Framework;
using NSubstitute;
using UnityEngine;

namespace Qwf.Client {
    [TestFixture]
    public class TestGamePiecePM {

        [Test]
        public void WhenCreatingPM_DefaultsToVisible() {
            GamePiecePM systemUnderTest = new GamePiecePM( Substitute.For<IGamePieceData>(), string.Empty );

            Assert.AreEqual( 1f, systemUnderTest.ViewModel.GetPropertyValue<float>( GamePiecePM.VISIBLE_PROPERTY ) );
        }

        [Test]
        public void WhenCreatingPM_ValueMatchesData() {
            IGamePieceData mockData = Substitute.For<IGamePieceData>();
            mockData.GetValue().Returns( 100 );

            GamePiecePM systemUnderTest = new GamePiecePM( mockData, string.Empty );

            Assert.AreEqual( 100, systemUnderTest.ViewModel.GetPropertyValue<int>( GamePiecePM.VALUE_PROPERTY ) );
        }

        [Test]
        public void WhenCreatingPM_IconValueMatchesData() {
            IGamePieceData mockData = Substitute.For<IGamePieceData>();
            mockData.GetPieceType().Returns( 1 );

            GamePiecePM systemUnderTest = new GamePiecePM( mockData, string.Empty );

            Assert.AreEqual( "1", systemUnderTest.ViewModel.GetPropertyValue<string>( GamePiecePM.ICON_PROPERTY ) );
        }

        [Test]
        public void WhenOwnerIsViewingPM_OutlineIsBlue() {
            IGamePieceData mockData = Substitute.For<IGamePieceData>();
            mockData.GetOwnerId().Returns( "Joe" );

            GamePiecePM systemUnderTest = new GamePiecePM( mockData, "Joe" );

            Assert.AreEqual( new Color(0, 0, 255), systemUnderTest.ViewModel.GetPropertyValue<Color>( GamePiecePM.OUTLINE_PROPERTY ) );
        }

        [Test]
        public void WhenOwnerIsNotViewingPM_OutlineIsRed() {
            IGamePieceData mockData = Substitute.For<IGamePieceData>();
            mockData.GetOwnerId().Returns( "Joe" );

            GamePiecePM systemUnderTest = new GamePiecePM( mockData, "NotJoe" );

            Assert.AreEqual( new Color(255, 0, 0 ), systemUnderTest.ViewModel.GetPropertyValue<Color>( GamePiecePM.OUTLINE_PROPERTY ) );
        }

        [Test]
        public void WhenUpdatingWithMissingPiece_PropertiesAsExpect() {
            GamePiecePM systemUnderTest = new GamePiecePM( null, "Joe" );

            Assert.AreEqual( new Color( 255, 0, 0 ), systemUnderTest.ViewModel.GetPropertyValue<Color>( GamePiecePM.OUTLINE_PROPERTY ) );
            Assert.AreEqual( "0", systemUnderTest.ViewModel.GetPropertyValue<string>( GamePiecePM.ICON_PROPERTY ) );
            Assert.AreEqual( 0, systemUnderTest.ViewModel.GetPropertyValue<int>( GamePiecePM.VALUE_PROPERTY ) );
            Assert.AreEqual( 0f, systemUnderTest.ViewModel.GetPropertyValue<float>( GamePiecePM.VISIBLE_PROPERTY ) );
        }

        [Test]
        public void WhenSettingPropertiesWithNullPieceData_GamePieceVariableIsNull() {
            GamePiecePM systemUnderTest = new GamePiecePM( null, "Joe" );

            Assert.IsNull( systemUnderTest.GamePiece );
        }
    }
}
