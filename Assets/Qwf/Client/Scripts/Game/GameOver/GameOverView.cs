using MyLibrary;

namespace Qwf.Client {
    public class GameOverView : GroupView {
        private GameOverPM mPM;

        void Start() {
            mPM = new GameOverPM();
            Init( mPM.ViewModel );
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            mPM.Dispose();
        }
    }
}