using MyLibrary;

namespace Qwf.Client {
    public class MatchMakerView : GroupView {
        public bool IsLocal;

        private MatchMakerPM mPM;

        void Start() {
            MatchMaker matcher = new MatchMaker();
            matcher.BeginMatchMakingProcess( IsLocal );

            mPM = new MatchMakerPM();
            SetModel( mPM.ViewModel );
        }      

        protected override void OnDestroy() {
            base.OnDestroy();

            mPM.Dispose();
        }
    }
}