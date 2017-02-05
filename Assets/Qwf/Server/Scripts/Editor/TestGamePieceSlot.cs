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
            IGamePiece incomingPiece = Substitute.For<IGamePiece>();
            incomingPiece.MatchesPieceType( Arg.Any<int>() ).Returns( false );

            bool canPlace = mSystemUnderTest.CanPlacePieceIntoSlot( incomingPiece );

            Assert.IsFalse( canPlace );
        }

        [Test]
        public void IfIncomingPieceTypeMatchesSlotType_AndSlotIsEmpty_CanPlace() {
            IGamePiece incomingPiece = Substitute.For<IGamePiece>();
            incomingPiece.MatchesPieceType( Arg.Any<int>() ).Returns( true );

            bool canPlace = mSystemUnderTest.CanPlacePieceIntoSlot( incomingPiece );

            Assert.IsTrue( canPlace );
        }

        [Test]
        public void IfIncomingPieceTypeMatchesSlotType_ButSamePlayerAlreadyOwnsSlot_CannotPlace() {
            IGamePiece incomingPiece = Substitute.For<IGamePiece>();
            incomingPiece.MatchesPieceType( Arg.Any<int>() ).Returns( true );
            
            IGamePiece currentPieceInSlot = Substitute.For<IGamePiece>();
            currentPieceInSlot.DoOwnersMatch( Arg.Any<string>() ).Returns( true );
            mSystemUnderTest.PlacePieceIntoSlot( currentPieceInSlot );

            bool canPlace = mSystemUnderTest.CanPlacePieceIntoSlot( incomingPiece );

            Assert.IsFalse( canPlace );
        }

        [Test]
        public void IfIncomingPieceTypeMatchesSlotType_AndIncomingPlayerDoesNotOwnSlot_CanPlaceIfHigherValuePiece() {
            IGamePiece incomingPiece = Substitute.For<IGamePiece>();
            incomingPiece.MatchesPieceType( Arg.Any<int>() ).Returns( true );            
            incomingPiece.CanOvertakePiece( Arg.Any<IGamePiece>() ).Returns( true );

            IGamePiece currentPieceInSlot = Substitute.For<IGamePiece>();
            currentPieceInSlot.DoOwnersMatch( Arg.Any<string>() ).Returns( false );
            mSystemUnderTest.PlacePieceIntoSlot( currentPieceInSlot );

            bool canPlace = mSystemUnderTest.CanPlacePieceIntoSlot( incomingPiece );

            Assert.IsTrue( canPlace );
        }

        [Test]
        public void IfIncomingPieceTypeMatchesSlotType_AndIncomingPlayerDoesNotOwnSlot_CannotPlaceIfLowerValuePiece() {
            IGamePiece incomingPiece = Substitute.For<IGamePiece>();
            incomingPiece.MatchesPieceType( Arg.Any<int>() ).Returns( true );
            incomingPiece.CanOvertakePiece( Arg.Any<IGamePiece>() ).Returns( false );

            IGamePiece currentPieceInSlot = Substitute.For<IGamePiece>();
            currentPieceInSlot.DoOwnersMatch( Arg.Any<string>() ).Returns( false );
            mSystemUnderTest.PlacePieceIntoSlot( currentPieceInSlot );

            bool canPlace = mSystemUnderTest.CanPlacePieceIntoSlot( incomingPiece );

            Assert.IsFalse( canPlace );
        }

        [Test]
        public void IfSlotHasCurrentPiece_IsEmptyIsFalse() {
            IGamePiece currentPieceInSlot = Substitute.For<IGamePiece>();
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
        public void WhenSlotIsScored_PieceIsScored() {
            mSystemUnderTest.PlacePieceIntoSlot( Substitute.For<IGamePiece>() );
            mSystemUnderTest.Score( Substitute.For<IScoreKeeper>() );

            mSystemUnderTest.GetCurrentPiece().Received().Score( Arg.Any<IScoreKeeper>() );
        }
    }
}