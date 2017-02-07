using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;

#pragma warning disable 0219

namespace Qwf.Client {
    [TestFixture]
    public class TestPassTurnPM : QwfUnitTest {
        [Test]
        public void WhenCreatingPM_IsVisibleProperty_TrueByDefault() {
            PassTurnPM systemUnderTest = new PassTurnPM();

            Assert.AreEqual( 1f, systemUnderTest.ViewModel.GetPropertyValue<float>( PassTurnPM.VISIBLE_PROPERTY ) );
        }
    }
}