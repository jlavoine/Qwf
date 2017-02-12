using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;

#pragma warning disable 0414

namespace Qwf.Server.UnitTests {
    [TestFixture]
    public class TestGamePlayer {
        private const int PLAYER_HAND_SIZE = 6;
        private const int TOTAL_GAME_PIECES = 30;

        [Test]
        public void AfterPlayIsCreated_HeldAndUndrawnPieces_MatchOriginalData() {
            IGameRules mockRules = GetStandardRules();
            List<IServerGamePiece> mockPieces = CreateGamePieces( TOTAL_GAME_PIECES );
            GamePlayer systemUnderTest = new GamePlayer( mockRules, mockPieces, string.Empty );

            Assert.AreEqual( PLAYER_HAND_SIZE, systemUnderTest.GetHeldPieces().Count );
            Assert.AreEqual( TOTAL_GAME_PIECES - PLAYER_HAND_SIZE, systemUnderTest.GetUndrawnPieces().Count );

            foreach ( IServerGamePiece piece in systemUnderTest.GetHeldPieces() ) {
                Assert.Contains( piece, mockPieces );
            }

            foreach ( IServerGamePiece piece in systemUnderTest.GetUndrawnPieces() ) {
                Assert.Contains( piece, mockPieces );
            }
        }

        [Test]
        public void IfUndrawnPiecesAreNotEnough_PlayerDrawsRemainingPieces() {
            IGameRules mockRules = GetStandardRules();
            List<IServerGamePiece> mockPieces = CreateGamePieces( PLAYER_HAND_SIZE - 1 );
            GamePlayer systemUnderTest = new GamePlayer( mockRules, mockPieces, string.Empty );

            Assert.AreEqual( mockPieces.Count, systemUnderTest.GetHeldPieces().Count );
            Assert.AreEqual( 0, systemUnderTest.GetUndrawnPieces().Count );
        }

        [Test]
        public void IsGamePieceHeld_ReturnsTrue_WhenGamePieceHeld() {
            IGameRules mockRules = GetStandardRules();
            List<IServerGamePiece> mockPieces = CreateGamePieces( 1 );
            GamePlayer systemUnderTest = new GamePlayer( mockRules, mockPieces, string.Empty );

            bool isPieceHeld = systemUnderTest.IsGamePieceHeld( mockPieces[0] );

            Assert.IsTrue( isPieceHeld );
        }

        [Test]
        public void IsGamePieceHeld_ReturnsFalse_WhenGamePieceNotHeld() {
            IGameRules mockRules = GetStandardRules();
            List<IServerGamePiece> mockPieces = CreateGamePieces( 1 );
            GamePlayer systemUnderTest = new GamePlayer( mockRules, mockPieces, string.Empty );

            IServerGamePiece unheldGamePiece = Substitute.For<IServerGamePiece>();
            bool isPieceHeld = systemUnderTest.IsGamePieceHeld( unheldGamePiece );

            Assert.IsFalse( isPieceHeld );
        }

        [Test]
        public void RemovingPieceFromHand_IfPlayerHoldsPiece_ReducesHandSize() {
            IGameRules mockRules = GetStandardRules();
            List<IServerGamePiece> mockPieces = CreateGamePieces( PLAYER_HAND_SIZE );
            GamePlayer systemUnderTest = new GamePlayer( mockRules, mockPieces, string.Empty );

            systemUnderTest.RemovePieceFromHand( mockPieces[0] );

            Assert.AreEqual( PLAYER_HAND_SIZE - 1, systemUnderTest.GetHeldPieces().Count );
        }

        [Test]
        public void RemovingPieceFromHand_IfPlayerDoesNotHoldPiece_DoesNotReducesHandSize() {
            IGameRules mockRules = GetStandardRules();
            List<IServerGamePiece> mockPieces = CreateGamePieces( PLAYER_HAND_SIZE );
            GamePlayer systemUnderTest = new GamePlayer( mockRules, mockPieces, string.Empty );

            systemUnderTest.RemovePieceFromHand( Substitute.For<IServerGamePiece>() );

            Assert.AreEqual( PLAYER_HAND_SIZE, systemUnderTest.GetHeldPieces().Count );
        }

        [Test]
        public void DrawToFillHand_FillsHand() {
            IGameRules mockRules = GetStandardRules();
            List<IServerGamePiece> mockPieces = CreateGamePieces( PLAYER_HAND_SIZE * 2 );
            GamePlayer systemUnderTest = new GamePlayer( mockRules, mockPieces, string.Empty );

            systemUnderTest.RemovePieceFromHand( mockPieces[0] );
            systemUnderTest.DrawToFillHand();            

            Assert.AreEqual( PLAYER_HAND_SIZE, systemUnderTest.GetHeldPieces().Count );
        }

        static object[] GetHeldPieceOfIndexTests = {
            new object[] { -1 },    // outside of bounds
            new object[] { 1 },     // outside of bounds
            new object[] { 0 }      // within bounds
        };

        [Test, TestCaseSource( "GetHeldPieceOfIndexTests" )]
        public void GetHeldPieceOfIndex_ReturnsExpectedValue( int i_index ) {
            IGameRules mockRules = Substitute.For<IGameRules>();
            mockRules.GetPlayerHandSize().Returns( 3 );
            List<IServerGamePiece> mockHand = new List<IServerGamePiece>();
            IServerGamePiece mockPiece = Substitute.For<IServerGamePiece>();
            mockHand.Add( mockPiece );

            GamePlayer systemUnderTest = new GamePlayer( mockRules, mockHand, string.Empty );

            if ( i_index != 0 ) {
                Assert.AreEqual( null, systemUnderTest.GetHeldPieceOfIndex( i_index ) );
            } else {
                Assert.AreEqual( mockPiece, systemUnderTest.GetHeldPieceOfIndex( i_index ) );
            }
        }

        private IGameRules GetStandardRules() {
            IGameRules mockRules = Substitute.For<IGameRules>();
            mockRules.GetPlayerHandSize().Returns( PLAYER_HAND_SIZE );

            return mockRules;
        }

        private List<IServerGamePiece> CreateGamePieces( int i_pieceCount ) {
            List<IServerGamePiece> mockPieces = new List<IServerGamePiece>();
            for ( int i = 0; i < i_pieceCount; ++i ) {
                mockPieces.Add( Substitute.For<IServerGamePiece>() );
            }

            return mockPieces;
        }
    }
}