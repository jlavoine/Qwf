using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;

#pragma warning disable 0219

namespace Qwf.Client {
    public class MakeMovePM_Stub : MakeMovePM { }

    [TestFixture]
    public class TestMakeMovePM : QwfUnitTest {
        [Test]
        public void WhenPassingTurn_TurnUpdateIsSentOut_WithThisPlayAsNotActive() {
            BackendManager.Instance.GetPlayerId().Returns( "Me" );
            MakeMovePM systemUnderTest = new MakeMovePM_Stub();

            systemUnderTest.ProcessAction();

            MyMessenger.Instance.Received().Send<ITurnUpdate>( ClientMessages.UPDATE_TURN, Arg.Is<ITurnUpdate>( activePlayer => activePlayer.IsThisPlayerActive() == false ) );
        }
    }
}
