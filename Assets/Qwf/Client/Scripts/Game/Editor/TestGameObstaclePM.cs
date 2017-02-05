using NUnit.Framework;
using NSubstitute;
using UnityEngine;

namespace Qwf.Client {
    [TestFixture]
    public class TestGameObstaclePM {

        [Test]
        public void WhenUpdating_PropertiesSetToExpected() {
            IGameObstacleUpdate mockUpdate = Substitute.For<IGameObstacleUpdate>();
            mockUpdate.GetImageKey().Returns( "TestKey" );
            mockUpdate.GetFinalBlowValue().Returns( 5 );

            GameObstaclePM systemUnderTest = new GameObstaclePM( mockUpdate );

            Assert.AreEqual( "TestKey", systemUnderTest.ViewModel.GetPropertyValue<string>( GameObstaclePM.IMAGE_PROPERTY ) );
            Assert.AreEqual( GameObstaclePM.FINAL_BLOW_PREFIX + "5", systemUnderTest.ViewModel.GetPropertyValue<string>( GameObstaclePM.FINAL_BLOW_PROPERTY ) );
            Assert.AreEqual( 1f, systemUnderTest.ViewModel.GetPropertyValue<float>( GameObstaclePM.VISIBLE_PROPERTY ) );
        }

        [Test]
        public void ImagePropertySet_WithUpdateImageKey() {
            IGameObstacleUpdate mockUpdate = Substitute.For<IGameObstacleUpdate>();
            mockUpdate.GetImageKey().Returns( "TestKey" );

            GameObstaclePM systemUnderTest = new GameObstaclePM( mockUpdate );

            Assert.AreEqual( "TestKey", systemUnderTest.ViewModel.GetPropertyValue<string>( GameObstaclePM.IMAGE_PROPERTY ) );
        }

        [Test]
        public void FinalBlowPropertySet_ToValueWithPlusSign() {
            IGameObstacleUpdate mockUpdate = Substitute.For<IGameObstacleUpdate>();
            mockUpdate.GetFinalBlowValue().Returns( 5 );

            GameObstaclePM systemUnderTest = new GameObstaclePM( mockUpdate );

            Assert.AreEqual( GameObstaclePM.FINAL_BLOW_PREFIX + "5", systemUnderTest.ViewModel.GetPropertyValue<string>( GameObstaclePM.FINAL_BLOW_PROPERTY ) );
        }

        [Test]
        public void WhenCreatingPM_DefaultsToVisible() {
            GameObstaclePM systemUnderTest = new GameObstaclePM( Substitute.For<IGameObstacleUpdate>() );

            Assert.AreEqual( 1f, systemUnderTest.ViewModel.GetPropertyValue<float>( GameObstaclePM.VISIBLE_PROPERTY ) );
        }

        [Test]
        public void WhenCreatingPM_SlotPMsCountMatchesData() {
            IGameObstacleUpdate mockUpdate = Substitute.For<IGameObstacleUpdate>();
            mockUpdate.GetSlotCount().Returns( 5 );

            GameObstaclePM systemUnderTest = new GameObstaclePM( mockUpdate );

            Assert.AreEqual( 5, systemUnderTest.SlotPiecePMs.Count );
        }
    }
}