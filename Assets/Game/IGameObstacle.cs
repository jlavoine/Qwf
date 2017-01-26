﻿using System.Collections.Generic;

namespace Qwf {
    public interface IGameObstacle {
        GameObstacleData GetData();

        List<IGamePieceSlot> GetSlots();

        bool CanPieceBePlacedIntoSlot( IGamePiece i_piece, IGamePieceSlot i_slot );
        bool IsComplete();
    }
}