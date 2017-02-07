using MyLibrary;

namespace Qwf.Client {
    public class ResetMovesView : GroupView {

       void Start() {
            ResetMovesPM pm = new ResetMovesPM();
            Init( pm.ViewModel );
        }
    }
}
