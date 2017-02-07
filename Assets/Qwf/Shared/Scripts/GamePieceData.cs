
namespace Qwf {
    public class GamePieceData : IGamePieceData {
        public int PieceType;
        public int Value;
        public string Owner;

        public static GamePieceData Create( IServerGamePiece i_piece ) {
            GamePieceData data = new GamePieceData();
            data.Value = i_piece.GetValue();
            data.PieceType = i_piece.GetPieceType();
            data.Owner = i_piece.GetOwner().Id;

            return data;
        }

        public string GetOwnerId() {
            return Owner;
        }

        public int GetValue() {
            return Value;
        }

        public int GetPieceType() {
            return PieceType;
        }
    }
}