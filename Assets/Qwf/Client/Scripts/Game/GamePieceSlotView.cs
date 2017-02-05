using MyLibrary;

namespace Qwf.Client {
    public class GamePieceSlotView : GroupView {

        public void Init( GamePieceSlotPM i_pm ) {
            SetModel( i_pm.ViewModel );
        }
    }
}