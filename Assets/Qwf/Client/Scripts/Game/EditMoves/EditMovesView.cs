﻿using MyLibrary;

namespace Qwf.Client {
    public class EditMovesView : GroupView {
        private EditMovesPM mPM;      

        void Start() {
            mPM = new EditMovesPM();
            SetModel( mPM.ViewModel );
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            mPM.Dispose();
        }
    }
}