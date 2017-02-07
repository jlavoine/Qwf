﻿
namespace Qwf.Client {
    public interface IGamePiecePM {
        IGamePiece GamePiece { get; }

        void SetProperties( IGamePieceData i_data );
        void SetVisibility( bool is_visible );

        int GetValue();
    }
}