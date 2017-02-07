using MyLibrary;

namespace Qwf.Client {
    public class GamePieceView : GroupView {
        private GamePiecePM mPM;
        public GamePiecePM PM { get { return mPM; } private set { mPM = value; } }

        public void Init( GamePiecePM i_pm ) {
            PM = i_pm;
            SetModel( i_pm.ViewModel );
        }
    }
}
