using MyLibrary;

namespace Qwf.Client {
    public class TurnDisplayView : GroupView {

        void Start() {
            TurnDisplayPM pm = new TurnDisplayPM();
            SetModel( pm.ViewModel );
        }
    }
}
