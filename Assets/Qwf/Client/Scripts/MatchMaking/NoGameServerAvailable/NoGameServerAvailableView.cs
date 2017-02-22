using MyLibrary;

namespace Qwf.Client {
    public class NoGameServerAvailableView : GroupView {
        private NoGameSeverAvailablePM mPM;

        void Start() {
            mPM = new NoGameSeverAvailablePM();

            SetModel( mPM.ViewModel );
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            mPM.Dispose();
        }
    }
}