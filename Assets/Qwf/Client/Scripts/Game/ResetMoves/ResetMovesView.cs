using MyLibrary;

namespace Qwf.Client {
    public class ResetMovesView : GroupView {
        private ResetMovesPM mPM;

        void Start() {
            mPM = new ResetMovesPM();
            Init( mPM.ViewModel );
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            mPM.Dispose();
        }

        public void OnClick() {
            mPM.ResetMoves();
        }
    }
}
