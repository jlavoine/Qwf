using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;

#pragma warning disable 0219

namespace Qwf.Client {
    [TestFixture]
    public class TestPlayerHandGamePiecePM : QwfUnitTest {
        [Test]
        public void WhenCreating_SubscribesToMessages() {
            PlayerHandGamePiecePM systemUnderTest = new PlayerHandGamePiecePM( Substitute.For<IGamePieceData>(), "" );

            MyMessenger.Instance.Received().AddListener( ClientGameEvents.MAX_MOVES_MADE, Arg.Any<Callback>() );
        }

        [Test]
        public void WhenDisposing_UnsubscribesFromMessages() {
            PlayerHandGamePiecePM systemUnderTest = new PlayerHandGamePiecePM( Substitute.For<IGamePieceData>(), "" );

            systemUnderTest.Dispose();

            MyMessenger.Instance.Received().RemoveListener( ClientGameEvents.MAX_MOVES_MADE, Arg.Any<Callback>() );
        }
    }
}