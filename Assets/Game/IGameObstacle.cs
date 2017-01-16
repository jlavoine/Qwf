using System.Collections.Generic;

namespace Qwf {
    public interface IGameObstacle {
        GameObstacleData GetData();

        List<IGamePieceSlot> GetSlots();
    }
}