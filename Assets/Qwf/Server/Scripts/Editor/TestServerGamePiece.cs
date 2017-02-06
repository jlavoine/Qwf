using NUnit.Framework;
using NSubstitute;

namespace Qwf {
    [TestFixture]
    public class TestServerGamePiece {
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
        public void IfOwnerDoesNotHoldPiece_PieceIsHeldReturnsFalse() {
            mMockOwner.IsGamePieceHeld( Arg.Any<IServerGamePiece>() ).Returns( false );
            GamePieceData data = new GamePieceData() { PieceType = PIECE_TYPE_A, Value = PIECE_VALUE };
            ServerGamePiece systemUnderTest = new ServerGamePiece( mMockOwner, data );

            Assert.IsFalse( systemUnderTest.IsCurrentlyHeld() );
        }

        [Test]
        public void IfOwnerHoldsPieces_PieceIsHeldReturnsTrue() {
            mMockOwner.IsGamePieceHeld( Arg.Any<IServerGamePiece>() ).Returns( true );
            GamePieceData data = new GamePieceData() { PieceType = PIECE_TYPE_A, Value = PIECE_VALUE };
            ServerGamePiece systemUnderTest = new ServerGamePiece( mMockOwner, data );

            Assert.IsTrue( systemUnderTest.IsCurrentlyHeld() );
        }

        [Test]
        public void PlacingPieceIntoSlot_RemovesFromPlayerHand_AndPlacesIntoSlot() {
            GamePieceData data = new GamePieceData() { PieceType = PIECE_TYPE_A, Value = PIECE_VALUE };
            ServerGamePiece systemUnderTest = new ServerGamePiece( mMockOwner, data );
            IGamePieceSlot mockSlot = Substitute.For<IGamePieceSlot>();

            systemUnderTest.PlaceFromPlayerHandIntoSlot( mockSlot );

            mockSlot.Received( 1 ).PlacePieceIntoSlot( systemUnderTest );
            mMockOwner.Received( 1 ).RemovePieceFromHand( systemUnderTest );
        }

        [Test]
        public void WhenScoringPiece_PointsAwardedToOwner() {
            GamePieceData data = new GamePieceData() { PieceType = PIECE_TYPE_A, Value = PIECE_VALUE };
            ServerGamePiece systemUnderTest = new ServerGamePiece( mMockOwner, data );
            IScoreKeeper mockScoreKeeper = Substitute.For<IScoreKeeper>();

            systemUnderTest.Score( mockScoreKeeper );

            mockScoreKeeper.Received().AddPointsToPlayer( mMockOwner, systemUnderTest.GetValue() );
        }

        [Test]
        public void WhenOwnersMatch_DoOwnersMatchCall_ReturnsTrue() {
            GamePieceData data = new GamePieceData() { PieceType = PIECE_TYPE_A, Value = PIECE_VALUE, Owner = "Me" };
            ServerGamePiece systemUnderTest = new ServerGamePiece( mMockOwner, data );

            bool match = systemUnderTest.DoOwnersMatch( "Me" );

            Assert.IsTrue( match );
        }

        [Test]
        public void WhenOwnersDoNotMatch_DoOwnersMatchCall_ReturnsFalse() {
            mMockOwner.Id.Returns( "Me" );
            GamePieceData data = new GamePieceData() { PieceType = PIECE_TYPE_A, Value = PIECE_VALUE };
            ServerGamePiece systemUnderTest = new ServerGamePiece( mMockOwner, data );

            bool match = systemUnderTest.DoOwnersMatch( "Them" );

            Assert.IsFalse( match );
        }
    }
}
