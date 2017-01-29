using System.Collections.Generic;

namespace Qwf {
    public class PlayerDeckData {
        public List<DeckGamePieceData> GamePieces;

        public List<IGamePiece> GetListOfPiecesFromDeck( IGamePlayer i_owner ) {
            List<IGamePiece> allPieces = new List<IGamePiece>();

            foreach ( DeckGamePieceData onePieceType in GamePieces ) {
                foreach ( KeyValuePair<int,int> pieceValueToCount in onePieceType.PieceValueToCount ) {
                    for ( int i = 0; i < pieceValueToCount.Value; ++i ) {
                        allPieces.Add( new GamePiece( i_owner, onePieceType.PieceType, pieceValueToCount.Key ) );
                    }
                }
            }

            return allPieces;
        }
    }
}