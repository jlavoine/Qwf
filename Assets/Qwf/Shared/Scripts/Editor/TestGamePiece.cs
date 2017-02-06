using NUnit.Framework;
using NSubstitute;

namespace Qwf {
    [TestFixture]
    public class TestGamePiece {
        private const int PIECE_TYPE_A = 1;
        private const int PIECE_TYPE_B = 2;
        private const int PIECE_TYPE_ANY = 0;

        private const int PIECE_VALUE = 10;

        [Test]
        public void IfIncomingTypeMatchesPieceType_MatchesIsTrue() {
            GamePieceData data = new GamePieceData() { PieceType = PIECE_TYPE_A };
            GamePiece systemUnderTest = new GamePiece( data );

            bool doesMatch = systemUnderTest.MatchesPieceType( PIECE_TYPE_A );

            Assert.IsTrue( doesMatch );
        }

        [Test]
        public void IfIncomingTypeDoesNotMatchPieceType_MatchesIsFalse() {
            GamePieceData data = new GamePieceData() { PieceType = PIECE_TYPE_A };
            GamePiece systemUnderTest = new GamePiece( data );

            bool doesMatch = systemUnderTest.MatchesPieceType( PIECE_TYPE_B );

            Assert.IsFalse( doesMatch );
        }

        [Test]
        public void IfIncomingTypeIsAny_MatchesIsTrue() {
            GamePieceData data = new GamePieceData() { PieceType = PIECE_TYPE_A };
            GamePiece systemUnderTest = new GamePiece( data );

            bool doesMatch = systemUnderTest.MatchesPieceType( PIECE_TYPE_ANY );

            Assert.IsTrue( doesMatch );
        }

        [Test]
        public void IfPieceTypeIsAny_MatchesNonAnyType() {
            GamePieceData data = new GamePieceData() { PieceType = PIECE_TYPE_ANY };
            GamePiece systemUnderTest = new GamePiece( data );

            bool doesMatch = systemUnderTest.MatchesPieceType( PIECE_TYPE_B );

            Assert.IsTrue( doesMatch );
        }

        [Test]
        public void IfIncomingPieceHasLowerValue_CanOvertake() {
            GamePieceData data = new GamePieceData() { PieceType = PIECE_TYPE_A, Value = PIECE_VALUE };
            GamePiece systemUnderTest = new GamePiece( data );

            IServerGamePiece incomingPiece = Substitute.For<IServerGamePiece>();
            incomingPiece.GetValue().Returns( PIECE_VALUE - 1 );

            bool canOvertake = systemUnderTest.CanOvertakePiece( incomingPiece );

            Assert.IsTrue( canOvertake );
        }

        [Test]
        public void IfIncomingPieceHasHigherValue_CannotOvertake() {
            GamePieceData data = new GamePieceData() { PieceType = PIECE_TYPE_A, Value = PIECE_VALUE };
            GamePiece systemUnderTest = new GamePiece( data );

            IServerGamePiece incomingPiece = Substitute.For<IServerGamePiece>();
            incomingPiece.GetValue().Returns( PIECE_VALUE + 1 );

            bool canOvertake = systemUnderTest.CanOvertakePiece( incomingPiece );

            Assert.IsFalse( canOvertake );
        }

        [Test]
        public void IfIncomingPieceHasEqualValue_CannotOvertake() {
            GamePieceData data = new GamePieceData() { PieceType = PIECE_TYPE_A, Value = PIECE_VALUE };
            GamePiece systemUnderTest = new GamePiece( data );

            IServerGamePiece incomingPiece = Substitute.For<IServerGamePiece>();
            incomingPiece.GetValue().Returns( PIECE_VALUE );

            bool canOvertake = systemUnderTest.CanOvertakePiece( incomingPiece );

            Assert.IsFalse( canOvertake );
        }
    }
}
