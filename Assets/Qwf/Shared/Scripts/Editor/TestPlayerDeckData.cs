using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;

namespace Qwf.UnitTests {
    [TestFixture]
    public class TestPlayerDeckData {
        [Test]
        public void CorrectListOfGamePieces_CreatedFromDeckData() {
            PlayerDeckData data = new PlayerDeckData();
            data.GamePieces = new List<DeckGamePieceData>();
            data.GamePieces.Add( new DeckGamePieceData() { PieceType = 0, PieceValueToCount = new Dictionary<int, int>() { { 1, 1 }, { 2, 2 } } } );
            data.GamePieces.Add( new DeckGamePieceData() { PieceType = 1, PieceValueToCount = new Dictionary<int, int>() { { 2, 1 }, { 4, 1 } } } );
            data.GamePieces.Add( new DeckGamePieceData() { PieceType = 2, PieceValueToCount = new Dictionary<int, int>() { { 3, 1 }, { 5, 3 } } } );
            
            IGamePlayer mockPlayer = Substitute.For<IGamePlayer>();
            List<IGamePiece> pieces = data.GetListOfPiecesFromDeck( mockPlayer );

            Assert.AreEqual( 9, pieces.Count );
            MakeSurePiecesOfTypeAreInList( pieces, 0, 1, 1 );
            MakeSurePiecesOfTypeAreInList( pieces, 0, 2, 2 );
            MakeSurePiecesOfTypeAreInList( pieces, 1, 2, 1 );
            MakeSurePiecesOfTypeAreInList( pieces, 1, 4, 1 );
            MakeSurePiecesOfTypeAreInList( pieces, 2, 3, 1 );
            MakeSurePiecesOfTypeAreInList( pieces, 2, 5, 3 );
        }

        private void MakeSurePiecesOfTypeAreInList( List<IGamePiece> i_pieces, int i_pieceType, int i_pieceValue, int i_pieceCount ) {
            int count = 0;

            foreach ( IGamePiece piece in i_pieces ) {
                if ( piece.GetPieceType() == i_pieceType && piece.GetValue() == i_pieceValue ) {
                    count++;
                }
            }

            Assert.AreEqual( i_pieceCount, count );
        }
    }
}
