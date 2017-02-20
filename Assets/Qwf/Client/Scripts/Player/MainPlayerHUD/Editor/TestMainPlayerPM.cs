using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;

#pragma warning disable 0219

namespace Qwf.Client {
    [TestFixture]
    public class TestMainPlayerPM : QwfUnitTest {

        private IPlayerData mMockData;

        [SetUp]
        public void BeforeTest_() {
            mMockData = Substitute.For<IPlayerData>();
        }

        [Test]
        public void WhenCreated_ExpectedPropertiesSet() {
            mMockData.Gold.Returns( 100 );
            MainPlayerPM systemUnderTest = new MainPlayerPM( mMockData );

            Assert.AreEqual( "100", systemUnderTest.ViewModel.GetPropertyValue<string>( MainPlayerPM.GOLD_PROPERTY ) );
        }
    }
}
