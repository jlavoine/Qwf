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
    }
}