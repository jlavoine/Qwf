using NUnit.Framework;
using NSubstitute;
using UnityEngine;

namespace Qwf.Client {
    [TestFixture]
    public class TestGameObstaclePM {

        [Test]
        public void ImagePropertySet_WithUpdateImageKey() {
            IGameObstacleUpdate mockUpdate = Substitute.For<IGameObstacleUpdate>();
            mockUpdate.GetImageKey().Returns( "TestKey" );

            GameObstaclePM systemUnderTest = new GameObstaclePM( mockUpdate );

            Assert.AreEqual( "TestKey", systemUnderTest.ViewModel.GetPropertyValue<string>( GameObstaclePM.IMAGE_PROPERTY ) );
        }

        [Test]
        public void WhenCreatingPM_DefaultsToVisible() {
            GameObstaclePM systemUnderTest = new GameObstaclePM( Substitute.For<IGameObstacleUpdate>() );

            Assert.AreEqual( 1f, systemUnderTest.ViewModel.GetPropertyValue<float>( GameObstaclePM.VISIBLE_PROPERTY ) );
        }
    }
}