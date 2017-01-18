using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;

namespace Qwf.UnitTests {
    [TestFixture]
    public class TestGamePlayer {
        private const int PLAYER_HAND_SIZE = 6;
        private const int TOTAL_GAME_PIECES = 30;

        [Test]
        public void AfterPlayIsCreated_HeldAndUndrawnPieces_MatchOriginalData() {
            IGameRules mockRules = GetStandardRules();
            List<IGamePiece> mockPieces = CreateGamePieces( TOTAL_GAME_PIECES );
            GamePlayer systemUnderTest = new GamePlayer( mockRules, mockPieces );

            Assert.AreEqual( PLAYER_HAND_SIZE, systemUnderTest.GetHeldPieces().Count );
            Assert.AreEqual( TOTAL_GAME_PIECES - PLAYER_HAND_SIZE, systemUnderTest.GetUndrawnPieces().Count );

            foreach ( IGamePiece piece in systemUnderTest.GetHeldPieces() ) {
                Assert.Contains( piece, mockPieces );
            }

            foreach ( IGamePiece piece in systemUnderTest.GetUndrawnPieces() ) {
                Assert.Contains( piece, mockPieces );
            }
        }

        [Test]
        public void IfUndrawnPiecesAreNotEnough_PlayerDrawsRemainingPieces() {
            IGameRules mockRules = GetStandardRules();
            List<IGamePiece> mockPieces = CreateGamePieces( PLAYER_HAND_SIZE - 1 );
            GamePlayer systemUnderTest = new GamePlayer( mockRules, mockPieces );

            Assert.AreEqual( mockPieces.Count, systemUnderTest.GetHeldPieces().Count );
            Assert.AreEqual( 0, systemUnderTest.GetUndrawnPieces().Count );
        }

        [Test]
        public void IsGamePieceHeld_ReturnsTrue_WhenGamePieceHeld() {
            IGameRules mockRules = GetStandardRules();
            List<IGamePiece> mockPieces = CreateGamePieces( 1 );
            GamePlayer systemUnderTest = new GamePlayer( mockRules, mockPieces );

            bool isPieceHeld = systemUnderTest.IsGamePieceHeld( mockPieces[0] );

            Assert.IsTrue( isPieceHeld );
        }

        [Test]
        public void IsGamePieceHeld_ReturnsFalse_WhenGamePieceNotHeld() {
            IGameRules mockRules = GetStandardRules();
            List<IGamePiece> mockPieces = CreateGamePieces( 1 );
            GamePlayer systemUnderTest = new GamePlayer( mockRules, mockPieces );

            IGamePiece unheldGamePiece = Substitute.For<IGamePiece>();
            bool isPieceHeld = systemUnderTest.IsGamePieceHeld( unheldGamePiece );

            Assert.IsFalse( isPieceHeld );
        }

        [Test]
        public void RemovingPieceFromHand_IfPlayerHoldsPiece_ReducesHandSize() {
            IGameRules mockRules = GetStandardRules();
            List<IGamePiece> mockPieces = CreateGamePieces( PLAYER_HAND_SIZE );
            GamePlayer systemUnderTest = new GamePlayer( mockRules, mockPieces );

            systemUnderTest.RemovePieceFromHand( mockPieces[0] );

            Assert.AreEqual( PLAYER_HAND_SIZE - 1, systemUnderTest.GetHeldPieces().Count );
        }

        [Test]
        public void RemovingPieceFromHand_IfPlayerDoesNotHoldPiece_DoesNotReducesHandSize() {
            IGameRules mockRules = GetStandardRules();
            List<IGamePiece> mockPieces = CreateGamePieces( PLAYER_HAND_SIZE );
            GamePlayer systemUnderTest = new GamePlayer( mockRules, mockPieces );

            systemUnderTest.RemovePieceFromHand( Substitute.For<IGamePiece>() );

            Assert.AreEqual( PLAYER_HAND_SIZE, systemUnderTest.GetHeldPieces().Count );
        }

        [Test]
        public void DrawToFillHand_FillsHand() {
            IGameRules mockRules = GetStandardRules();
            List<IGamePiece> mockPieces = CreateGamePieces( PLAYER_HAND_SIZE * 2 );
            GamePlayer systemUnderTest = new GamePlayer( mockRules, mockPieces );

            systemUnderTest.RemovePieceFromHand( mockPieces[0] );
            systemUnderTest.DrawToFillHand();            

            Assert.AreEqual( PLAYER_HAND_SIZE, systemUnderTest.GetHeldPieces().Count );
        }

        private IGameRules GetStandardRules() {
            IGameRules mockRules = Substitute.For<IGameRules>();
            mockRules.GetPlayerHandSize().Returns( PLAYER_HAND_SIZE );

            return mockRules;
        }

        private List<IGamePiece> CreateGamePieces( int i_pieceCount ) {
            List<IGamePiece> mockPieces = new List<IGamePiece>();
            for ( int i = 0; i < i_pieceCount; ++i ) {
                mockPieces.Add( Substitute.For<IGamePiece>() );
            }

            return mockPieces;
        }
    }
}