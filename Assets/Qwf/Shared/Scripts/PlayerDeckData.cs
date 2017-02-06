using System.Collections.Generic;

namespace Qwf {
    public class PlayerDeckData {
        public List<DeckGamePieceData> GamePieces;

        public List<IServerGamePiece> GetListOfPiecesFromDeck( IGamePlayer i_owner ) {
            List<IServerGamePiece> allPieces = new List<IServerGamePiece>();

            foreach ( DeckGamePieceData onePieceType in GamePieces ) {
                foreach ( KeyValuePair<int,int> pieceValueToCount in onePieceType.PieceValueToCount ) {
                    for ( int i = 0; i < pieceValueToCount.Value; ++i ) {
                        IGamePieceData gamePieceData = new GamePieceData() { Value = pieceValueToCount.Key, Owner = i_owner.Id, PieceType = onePieceType.PieceType };
                        allPieces.Add( new ServerGamePiece( i_owner, gamePieceData ) );
                    }
                }
            }

            return allPieces;
        }
    }
}