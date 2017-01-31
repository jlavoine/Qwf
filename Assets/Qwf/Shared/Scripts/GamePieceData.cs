
namespace Qwf {
    public class GamePieceData {
        public int PieceType;
        public int Value;

        public static GamePieceData Create( IGamePiece i_piece ) {
            GamePieceData data = new GamePieceData();
            data.Value = i_piece.GetValue();
            data.PieceType = i_piece.GetPieceType();

            return data;
        }
    }
}