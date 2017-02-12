using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;

#pragma warning disable 0219

namespace Qwf.Client {
    [TestFixture]
    public class TestPlayerHandPM {

        [SetUp]
        public void BeforeTest() {
            MyMessenger.Instance = Substitute.For<IMessageService>();
        }

        [TearDown]
        public void AfterTest() {
            MyMessenger.Instance = null;
        }

        [Test]
        public void WhenCreating_SystemSubscribesToUpdate() {
            PlayerHandPM systemUnderTest = new PlayerHandPM( new List<IGamePieceData>(), string.Empty );

            MyMessenger.Instance.Received().AddListener<PlayerHandUpdateData>( ClientMessages.UPDATE_HAND, Arg.Any<Callback<PlayerHandUpdateData>>() );
        }

        [Test]
        public void WhenDisposing_SystemUnsubscribesToUpdate() {
            PlayerHandPM systemUnderTest = new PlayerHandPM( new List<IGamePieceData>(), string.Empty );

            systemUnderTest.Dispose();

            MyMessenger.Instance.Received().RemoveListener<PlayerHandUpdateData>( ClientMessages.UPDATE_HAND, Arg.Any<Callback<PlayerHandUpdateData>>() );
        }

        [Test]
        public void WhenCreatingPM_DefaultNumberOfGamePiecePMsAreCreated() {
            List<IGamePieceData> pieceData = new List<IGamePieceData>();
            pieceData.Add( Substitute.For<IGamePieceData>() );
            pieceData.Add( Substitute.For<IGamePieceData>() );
            pieceData.Add( Substitute.For<IGamePieceData>() );

            PlayerHandPM systemUnderTest = new PlayerHandPM( pieceData, string.Empty );

            Assert.AreEqual( new GameRules().GetPlayerHandSize(), systemUnderTest.GamePiecePMs.Count );
        }
    }
}