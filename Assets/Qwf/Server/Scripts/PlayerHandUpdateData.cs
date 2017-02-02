using System.Collections.Generic;

namespace Qwf {
    public class PlayerHandUpdateData : IPlayerHandUpdateData {
        public string Id;
        public List<GamePieceData> GamePieces;

        public static PlayerHandUpdateData Create( IGamePlayer i_player ) {
            PlayerHandUpdateData data = new PlayerHandUpdateData();
            data.Id = i_player.Id;

            data.GamePieces = new List<GamePieceData>();
            foreach ( IGamePiece piece in i_player.GetHeldPieces() ) {
                data.GamePieces.Add( GamePieceData.Create( piece ) );
            }

            return data;
        }
    }
}