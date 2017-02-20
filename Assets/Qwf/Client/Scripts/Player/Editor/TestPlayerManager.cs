using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;

#pragma warning disable 0414

namespace Qwf.Client {
    [TestFixture]
    public class TestPlayerManager : QwfUnitTest {

        private IQwfBackend mMockBackend;
        private IPlayerData mMockPlayerData;

        [SetUp]
        public void BeforeTest_() {            
            mMockBackend = Substitute.For<IQwfBackend>();
            mMockPlayerData = Substitute.For<IPlayerData>();

            BackendManager.Instance.GetBackend<IQwfBackend>().Returns( mMockBackend );
        }

        [Test]
        public void WhenInitingManager_GetVirtualCurrency_CalledOnBackend() {            
            PlayerManager systemUnderTest = new PlayerManager();
            systemUnderTest.Init( mMockPlayerData );

            mMockBackend.Received( 1 ).GetVirtualCurrency( PlayerManager.GOLD_KEY, Arg.Any<Callback<int>>() );
        }

        [Test]
        public void WhenManagerReceivesCurrency_GoldOnPlayerDataIsSet() {
            PlayerManager systemUnderTest = new PlayerManager();
            systemUnderTest.Init( mMockPlayerData );

            systemUnderTest.OnCurrencyRequest( 100 );

            Assert.AreEqual( 100, mMockPlayerData.Gold );
        }
    }
}