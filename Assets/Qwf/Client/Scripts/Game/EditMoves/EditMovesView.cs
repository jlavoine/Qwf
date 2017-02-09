using MyLibrary;

namespace Qwf.Client {
    public class EditMovesView : GroupView {

        void Start() {
            EditMovesPM pm = new EditMovesPM();
            SetModel( pm.ViewModel );
        }
    }
}