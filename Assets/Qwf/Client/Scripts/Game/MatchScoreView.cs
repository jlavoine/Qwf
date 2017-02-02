using UnityEngine;
using MyLibrary;
using System.Collections.Generic;

namespace Qwf.Client {
    public class MatchScoreView : GroupView {
        private MatchScorePM mPM;

        public void Init( MatchScorePM i_pm ) {
            mPM = i_pm;
            SetModel( i_pm.ViewModel );
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            mPM.Dispose();
        }
    }
}
