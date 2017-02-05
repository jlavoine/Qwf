using NUnit.Framework;
using NSubstitute;

namespace Qwf {
    [TestFixture]
    public class TestGamePiece {
        private const int PIECE_TYPE_A = 1;
        private const int PIECE_TYPE_B = 2;
        private const int PIECE_TYPE_ANY = 0;

        private const int PIECE_VALUE = 10;

        private IGamePlayer mMockOwner;

        [SetUp]
        public void BeforeTest() {
            mMockOwner = Substitute.For<IGamePlayer>();
        }

        [Test]
        public void IfIncomingTypeMatchesPieceType_MatchesIsTrue() {
            GamePieceData data = new GamePieceData() { PieceType = PIECE_TYPE_A };
            GamePiece systemUnderTest = new GamePiece( mMockOwner, data );

            bool doesMatch = systemUnderTest.MatchesPieceType( PIECE_TYPE_A );

            Assert.IsTrue( doesMatch );
        }

        [Test]
        public void IfIncomingTypeDoesNotMatchPieceType_MatchesIsFalse() {
            GamePieceData data = new GamePieceData() { PieceType = PIECE_TYPE_A };
            GamePiece systemUnderTest = new GamePiece( mMockOwner, data );

            bool doesMatch = systemUnderTest.MatchesPieceType( PIECE_TYPE_B );

            Assert.IsFalse( doesMatch );
        }

        [Test]
        public void IfIncomingTypeIsAny_MatchesIsTrue() {
            GamePieceData data = new GamePieceData() { PieceType = PIECE_TYPE_A };
            GamePiece systemUnderTest = new GamePiece( mMockOwner, data );

            bool doesMatch = systemUnderTest.MatchesPieceType( PIECE_TYPE_ANY );

            Assert.IsTrue( doesMatch );
        }

        [Test]
        public void IfPieceTypeIsAny_MatchesNonAnyType() {
            GamePieceData data = new GamePieceData() { PieceType = PIECE_TYPE_ANY };
            GamePiece systemUnderTest = new GamePiece( mMockOwner, data );

            bool doesMatch = systemUnderTest.MatchesPieceType( PIECE_TYPE_B );

            Assert.IsTrue( doesMatch );
        }

        [Test]
        public void IfIncomingPieceHasLowerValue_CanOvertake() {
            GamePieceData data = new GamePieceData() { PieceType = PIECE_TYPE_A, Value = PIECE_VALUE };
            GamePiece systemUnderTest = new GamePiece( mMockOwner, data );

            IGamePiece incomingPiece = Substitute.For<IGamePiece>();
            incomingPiece.GetValue().Returns( PIECE_VALUE - 1 );

            bool canOvertake = systemUnderTest.CanOvertakePiece( incomingPiece );

            Assert.IsTrue( canOvertake );
        }

        [Test]
        public void IfIncomingPieceHasHigherValue_CannotOvertake() {
            GamePieceData data = new GamePieceData() { PieceType = PIECE_TYPE_A, Value = PIECE_VALUE };
            GamePiece systemUnderTest = new GamePiece( mMockOwner, data );

            IGamePiece incomingPiece = Substitute.For<IGamePiece>();
            incomingPiece.GetValue().Returns( PIECE_VALUE + 1 );

            bool canOvertake = systemUnderTest.CanOvertakePiece( incomingPiece );

            Assert.IsFalse( canOvertake );
        }

        [Test]
        public void IfIncomingPieceHasEqualValue_CannotOvertake() {
            GamePieceData data = new GamePieceData() { PieceType = PIECE_TYPE_A, Value = PIECE_VALUE };
            GamePiece systemUnderTest = new GamePiece( mMockOwner, data );

            IGamePiece incomingPiece = Substitute.For<IGamePiece>();
            incomingPiece.GetValue().Returns( PIECE_VALUE );

            bool canOvertake = systemUnderTest.CanOvertakePiece( incomingPiece );

            Assert.IsFalse( canOvertake );
        }

        [Test]
        public void IfOwnerDoesNotHoldPiece_PieceIsHeldReturnsFalse() {
            mMockOwner.IsGamePieceHeld( Arg.Any<IGamePiece>() ).Returns( false );
            GamePieceData data = new GamePieceData() { PieceType = PIECE_TYPE_A, Value = PIECE_VALUE };
            GamePiece systemUnderTest = new GamePiece( mMockOwner, data );

            Assert.IsFalse( systemUnderTest.IsCurrentlyHeld() );
        }

        [Test]
        public void IfOwnerHoldsPieces_PieceIsHeldReturnsTrue() {
            mMockOwner.IsGamePieceHeld( Arg.Any<IGamePiece>() ).Returns( true );
            GamePieceData data = new GamePieceData() { PieceType = PIECE_TYPE_A, Value = PIECE_VALUE };
            GamePiece systemUnderTest = new GamePiece( mMockOwner, data );

            Assert.IsTrue( systemUnderTest.IsCurrentlyHeld() );
        }

        [Test]
        public void PlacingPieceIntoSlot_RemovesFromPlayerHand_AndPlacesIntoSlot() {
            GamePieceData data = new GamePieceData() { PieceType = PIECE_TYPE_A, Value = PIECE_VALUE };
            GamePiece systemUnderTest = new GamePiece( mMockOwner, data );
            IGamePieceSlot mockSlot = Substitute.For<IGamePieceSlot>();

            systemUnderTest.PlaceFromPlayerHandIntoSlot( mockSlot );

            mockSlot.Received( 1 ).PlacePieceIntoSlot( systemUnderTest );
            mMockOwner.Received( 1 ).RemovePieceFromHand( systemUnderTest );
        }

        [Test]
        public void WhenScoringPiece_PointsAwardedToOwner() {
            GamePieceData data = new GamePieceData() { PieceType = PIECE_TYPE_A, Value = PIECE_VALUE };
            GamePiece systemUnderTest = new GamePiece( mMockOwner, data );
            IScoreKeeper mockScoreKeeper = Substitute.For<IScoreKeeper>();

            systemUnderTest.Score( mockScoreKeeper );

            mockScoreKeeper.Received().AddPointsToPlayer( mMockOwner, systemUnderTest.GetValue() );
        }

        [Test]
        public void WhenOwnersMatch_DoOwnersMatchCall_ReturnsTrue() {
            mMockOwner.Id.Returns( "Me" );
            GamePieceData data = new GamePieceData() { PieceType = PIECE_TYPE_A, Value = PIECE_VALUE };
            GamePiece systemUnderTest = new GamePiece( mMockOwner, data );

            bool match = systemUnderTest.DoOwnersMatch( "Me" );

            Assert.IsTrue( match );
        }

        [Test]
        public void WhenOwnersDoNotMatch_DoOwnersMatchCall_ReturnsFalse() {
            mMockOwner.Id.Returns( "Me" );
            GamePieceData data = new GamePieceData() { PieceType = PIECE_TYPE_A, Value = PIECE_VALUE };
            GamePiece systemUnderTest = new GamePiece( mMockOwner, data );

            bool match = systemUnderTest.DoOwnersMatch( "Them" );

            Assert.IsFalse( match );
        }
    }
}
