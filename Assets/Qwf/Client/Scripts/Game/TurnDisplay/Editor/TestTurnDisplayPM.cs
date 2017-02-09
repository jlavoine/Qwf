using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;

#pragma warning disable 0219

namespace Qwf.Client {
    [TestFixture]
    public class TestTurnDisplayPM : QwfUnitTest {
        [Test]
        public void WhenCreating_SubscribesToMessages() {
            TurnDisplayPM systemUnderTest = new TurnDisplayPM();

            MyMessenger.Instance.Received().AddListener<ITurnUpdate>( ClientMessages.UPDATE_TURN, Arg.Any<Callback<ITurnUpdate>>() );
        }

        [Test]
        public void WhenDisposing_UnsubscribesToMessages() {
            TurnDisplayPM systemUnderTest = new TurnDisplayPM();

            systemUnderTest.Dispose();

            MyMessenger.Instance.Received().RemoveListener<ITurnUpdate>( ClientMessages.UPDATE_TURN, Arg.Any<Callback<ITurnUpdate>>() );
        }

        [Test]
        public void OnUpdate_IfPlayersTurn_MessageExpected() {
            BackendManager.Instance.GetPlayerId().Returns( "Me" );
            TurnDisplayPM systemUnderTest = new TurnDisplayPM();
            ITurnUpdate mockUpdate = Substitute.For<ITurnUpdate>();
            mockUpdate.GetActivePlayer().Returns( "Me" );

            systemUnderTest.OnTurnUpdate( mockUpdate );

            Assert.AreEqual( TurnDisplayPM.PLAYER_TURN_MESSAGE, systemUnderTest.ViewModel.GetPropertyValue<string>( TurnDisplayPM.DISPLAY_PROPERTY ) );
        }

        [Test]
        public void OnUpdate_IfOpponentsTurn_MessageExpected() {
            BackendManager.Instance.GetPlayerId().Returns( "Me" );
            TurnDisplayPM systemUnderTest = new TurnDisplayPM();
            ITurnUpdate mockUpdate = Substitute.For<ITurnUpdate>();
            mockUpdate.GetActivePlayer().Returns( "Them" );

            systemUnderTest.OnTurnUpdate( mockUpdate );

            Assert.AreEqual( TurnDisplayPM.OPPONENT_TURN_MESSAGE, systemUnderTest.ViewModel.GetPropertyValue<string>( TurnDisplayPM.DISPLAY_PROPERTY ) );
        }
    }
}
