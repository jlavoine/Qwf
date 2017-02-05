
namespace Qwf {
    public interface IGameObstacleUpdate  {
        string GetId();
        string GetImageKey();

        int GetFinalBlowValue();
        int GetSlotCount();

        IGamePieceSlotUpdate GetSlotUpdate( int i_index );
    }
}
