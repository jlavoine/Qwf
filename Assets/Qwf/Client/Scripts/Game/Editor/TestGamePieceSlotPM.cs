﻿using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using MyLibrary;
using System.Collections.Generic;

namespace Qwf.Client {
    [TestFixture]
    public class TestGamePieceSlotPM : QwfUnitTest {

        [Test]
        public void WhenCreated_VisibilityDefaultsOn() {
            GamePieceSlotPM systemUnderTest = new GamePieceSlotPM( Substitute.For<IGamePieceSlotUpdate>() );

            Assert.AreEqual( 1f, systemUnderTest.ViewModel.GetPropertyValue<float>( GamePieceSlotPM.VISIBLE_PROPERTY ) );
        }

        [Test]
        public void WhenCreated_SlotPieceTypePropertyMatchesData() {
            IGamePieceSlotUpdate mockUpdate = Substitute.For<IGamePieceSlotUpdate>();
            mockUpdate.GetPieceType().Returns( 3 );

            GamePieceSlotPM systemUnderTest = new GamePieceSlotPM( mockUpdate );

            Assert.AreEqual( "3", systemUnderTest.ViewModel.GetPropertyValue<string>( GamePieceSlotPM.SLOT_PIECE_TYPE_PROPERTY ) );
        }

        [Test]
        public void IfIncomingUpdateIsNull_PropertiesSetAsExpected() {
            GamePieceSlotPM systemUnderTest = new GamePieceSlotPM( null );

            Assert.AreEqual( "1", systemUnderTest.ViewModel.GetPropertyValue<string>( GamePieceSlotPM.SLOT_PIECE_TYPE_PROPERTY ) );
            Assert.AreEqual( 0f, systemUnderTest.ViewModel.GetPropertyValue<float>( GamePieceSlotPM.VISIBLE_PROPERTY ) );
        }

        [Test]
        public void WhenAttemptingToMovePieceIntoSlot_IfValidMove_PieceIsPlayed() {
            IGamePieceSlot mockSlot = Substitute.For<IGamePieceSlot>();
            mockSlot.CanPlacePieceIntoSlot( Arg.Any<IGamePiece>() ).Returns( true );
            GamePieceSlotPM systemUnderTest = new GamePieceSlotPM( null );
            systemUnderTest.Slot = mockSlot;

            IPlayerHandGamePiecePM mockPlayerHandPiece = Substitute.For<IPlayerHandGamePiecePM>();
            systemUnderTest.AttemptToPlayPieceInSlot( mockPlayerHandPiece );

            mockPlayerHandPiece.Received( 1 ).Play();
        }

        [Test]
        public void WhenAttemptingToMovePieceIntoSlot_IfValidMove_MoveMadeEventSent() {
            IGamePieceSlot mockSlot = Substitute.For<IGamePieceSlot>();
            mockSlot.GetObstacleIndex().Returns( 3 );
            mockSlot.GetIndex().Returns( 1 );
            mockSlot.CanPlacePieceIntoSlot( Arg.Any<IGamePiece>() ).Returns( true );

            IPlayerHandGamePiecePM mockHandPiece = Substitute.For<IPlayerHandGamePiecePM>();
            mockHandPiece.Index.Returns( 5 );

            GamePieceSlotPM systemUnderTest = new GamePieceSlotPM( null );
            systemUnderTest.Slot = mockSlot;
            systemUnderTest.AttemptToPlayPieceInSlot( mockHandPiece );

            MyMessenger.Instance.Received().Send<IClientMoveAttempt>( ClientGameEvents.MADE_MOVE, Arg.Is<IClientMoveAttempt>( attempt => attempt.GetObstacleIndex() == 3 && attempt.GetSlotIndex() == 1 && attempt.GetPlayerPieceIndex() == 5 ) );
        }

        [Test]
        public void WhenAttemptingToMovePieceIntoSlot_IfNotValidMove_PieceIsNotPlayed() {
            IGamePieceSlot mockSlot = Substitute.For<IGamePieceSlot>();
            mockSlot.CanPlacePieceIntoSlot( Arg.Any<IGamePiece>() ).Returns( false );
            GamePieceSlotPM systemUnderTest = new GamePieceSlotPM( null );
            systemUnderTest.Slot = mockSlot;

            IPlayerHandGamePiecePM mockPlayerHandPiece = Substitute.For<IPlayerHandGamePiecePM>();
            systemUnderTest.AttemptToPlayPieceInSlot( mockPlayerHandPiece );

            mockPlayerHandPiece.DidNotReceive().Play();
            mockPlayerHandPiece.Received().InvalidPlayAttempt();
        }
    }
}