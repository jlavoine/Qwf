using MyLibrary;

namespace Qwf.Client {
    public class SendMovesView : GroupView {
        private SendMovesPM mPM;

        void Start() {
            mPM = new SendMovesPM();
            Init( mPM.ViewModel );
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            mPM.Dispose();
        }

        public void OnClick() {
            
        }
    }
}
