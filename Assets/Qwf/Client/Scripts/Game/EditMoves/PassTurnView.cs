using MyLibrary;

namespace Qwf.Client {
    public class PassTurnView : GroupView {
        private PassTurnPM mPM;

        void Start() {
            mPM = new PassTurnPM();
            Init( mPM.ViewModel );
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            mPM.Dispose();
        }

        public void OnClick() {
            mPM.ProcessAction();
        }
    }
}