﻿
namespace Qwf {
    public class GamePieceData : IGamePieceData {
        public int PieceType;
        public int Value;
        public string Owner;

        public static GamePieceData Create( IGamePiece i_piece ) {
            if ( i_piece != null ) {
                GamePieceData data = new GamePieceData();
                data.Value = i_piece.GetValue();
                data.PieceType = i_piece.GetPieceType();
                data.Owner = i_piece.GetOwnerId();

                return data;
            } else {
                return null; // totally valid; just means there was no piece
            }
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