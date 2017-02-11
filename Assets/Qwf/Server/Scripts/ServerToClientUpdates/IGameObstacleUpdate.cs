
namespace Qwf {
    public interface IGameObstacleUpdate  {
        string GetId();
        string GetImageKey();

        int GetFinalBlowValue();
        int GetSlotCount();
        int GetIndex();

        IGamePieceSlotUpdate GetSlotUpdate( int i_index );
    }
}
