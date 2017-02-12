using NUnit.Framework;
using NSubstitute;

namespace Qwf {
    [TestFixture]
    public class TestGamePieceSlot {
        private GamePieceSlot mSystemUnderTest;

        [SetUp]
        public void BeforeTest() {
            GamePieceSlotData data = new GamePieceSlotData() { PieceType = 1, ScoreValue = 1 };
            mSystemUnderTest = new GamePieceSlot( data );
        }

        [Test]
        public void IfIncomingPieceTypeDoesNotMatchSlotType_CannotPlace() {
            IServerGamePiece incomingPiece = Substitute.For<IServerGamePiece>();
            incomingPiece.MatchesPieceType( Arg.Any<int>() ).Returns( false );

            bool canPlace = mSystemUnderTest.CanPlacePieceIntoSlot( incomingPiece );

            Assert.IsFalse( canPlace );
        }

        [Test]
        public void IfIncomingPieceTypeMatchesSlotType_AndSlotIsEmpty_CanPlace() {
            IServerGamePiece incomingPiece = Substitute.For<IServerGamePiece>();
            incomingPiece.MatchesPieceType( Arg.Any<int>() ).Returns( true );

            bool canPlace = mSystemUnderTest.CanPlacePieceIntoSlot( incomingPiece );

            Assert.IsTrue( canPlace );
        }

        [Test]
        public void IfIncomingPieceTypeMatchesSlotType_ButSamePlayerAlreadyOwnsSlot_CannotPlace() {
            IServerGamePiece incomingPiece = Substitute.For<IServerGamePiece>();
            incomingPiece.MatchesPieceType( Arg.Any<int>() ).Returns( true );
            
            IServerGamePiece currentPieceInSlot = Substitute.For<IServerGamePiece>();
            currentPieceInSlot.DoOwnersMatch( Arg.Any<string>() ).Returns( true );
            mSystemUnderTest.PlacePieceIntoSlot( currentPieceInSlot );

            bool canPlace = mSystemUnderTest.CanPlacePieceIntoSlot( incomingPiece );

            Assert.IsFalse( canPlace );
        }

        [Test]
        public void IfIncomingPieceTypeMatchesSlotType_AndIncomingPlayerDoesNotOwnSlot_CanPlaceIfHigherValuePiece() {
            IServerGamePiece incomingPiece = Substitute.For<IServerGamePiece>();
            incomingPiece.MatchesPieceType( Arg.Any<int>() ).Returns( true );            
            incomingPiece.CanOvertakePiece( Arg.Any<IServerGamePiece>() ).Returns( true );

            IServerGamePiece currentPieceInSlot = Substitute.For<IServerGamePiece>();
            currentPieceInSlot.DoOwnersMatch( Arg.Any<string>() ).Returns( false );
            mSystemUnderTest.PlacePieceIntoSlot( currentPieceInSlot );

            bool canPlace = mSystemUnderTest.CanPlacePieceIntoSlot( incomingPiece );

            Assert.IsTrue( canPlace );
        }

        [Test]
        public void IfIncomingPieceTypeMatchesSlotType_AndIncomingPlayerDoesNotOwnSlot_CannotPlaceIfLowerValuePiece() {
            IServerGamePiece incomingPiece = Substitute.For<IServerGamePiece>();
            incomingPiece.MatchesPieceType( Arg.Any<int>() ).Returns( true );
            incomingPiece.CanOvertakePiece( Arg.Any<IServerGamePiece>() ).Returns( false );

            IServerGamePiece currentPieceInSlot = Substitute.For<IServerGamePiece>();
            currentPieceInSlot.DoOwnersMatch( Arg.Any<string>() ).Returns( false );
            mSystemUnderTest.PlacePieceIntoSlot( currentPieceInSlot );

            bool canPlace = mSystemUnderTest.CanPlacePieceIntoSlot( incomingPiece );

            Assert.IsFalse( canPlace );
        }

        [Test]
        public void IfSlotHasCurrentPiece_IsEmptyIsFalse() {
            IServerGamePiece currentPieceInSlot = Substitute.For<IServerGamePiece>();
            mSystemUnderTest.PlacePieceIntoSlot( currentPieceInSlot );

            bool isEmpty = mSystemUnderTest.IsEmpty();

            Assert.IsFalse( isEmpty );
        }

        [Test]
        public void IfSlotHasNoCurrentPiece_IsEmptyIsTrue() {
            bool isEmpty = mSystemUnderTest.IsEmpty();

            Assert.IsTrue( isEmpty );
        }

        [Test]
        public void WhenSlotIsScored_PointsAwardedToPlayerWithPieceInSlot() {
            IScoreKeeper mockScoreKeeper = Substitute.For<IScoreKeeper>();
            IServerGamePiece mockPiece = Substitute.For<IServerGamePiece>();
            mockPiece.GetOwnerId().Returns( "Me" );

            mSystemUnderTest.PlacePieceIntoSlot( mockPiece );
            mSystemUnderTest.Score( mockScoreKeeper );

            mockScoreKeeper.Received().AddPointsToPlayer( "Me", 1 );
        }
    }
}