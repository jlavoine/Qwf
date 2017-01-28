using MyLibrary;

namespace Qwf.Client {
    public class GamePieceView : GroupView {

        public void Init( GamePiecePM i_pm ) {
            SetModel( i_pm.ViewModel );
        }
    }
}
