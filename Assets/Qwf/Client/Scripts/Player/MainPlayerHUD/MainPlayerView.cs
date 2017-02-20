using MyLibrary;

namespace Qwf.Client {
    public class MainPlayerView : GroupView {
        private MainPlayerPM mPM;

        void Start() {
            mPM = new MainPlayerPM( PlayerManager.Instance.Data );
            SetModel( mPM.ViewModel );
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            mPM.Dispose();
        }
    }
}
