using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;

namespace Qwf.Client {
    [TestFixture]
    public abstract class QwfUnitTest {

        [SetUp]
        public virtual void BeforeTest() {
            BackendManager.Instance = Substitute.For<IBackendManager>();
            MyMessenger.Instance = Substitute.For<IMessageService>();
            StringTableManager.Instance = Substitute.For<IStringTableManager>();
        }

        [TearDown]
        public virtual void AfterTest() {
            BackendManager.Instance = null;
            MyMessenger.Instance = null;
            StringTableManager.Instance = null;
        }
    }
}
