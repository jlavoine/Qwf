using UnityEngine;
using MyLibrary;
using System.Collections.Generic;

namespace Qwf.Client {
    public class MatchScoreView : GroupView {
        private MatchScorePM mPM;

        void Start() {
            Init();
        }

        public void Init() {
            mPM = new MatchScorePM( BackendManager.Instance.GetPlayerId() );
            SetModel( mPM.ViewModel );
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            mPM.Dispose();
        }
    }
}
