using UnityEngine;
using MyLibrary;
using System.Collections.Generic;

namespace Qwf.Client {
    public class PlayerHandView : GroupView {
        public GameObject GamePieceViewArea;
        public GameObject GamePieceViewPrefab;

        private List<GamePieceView> mPieceViews = new List<GamePieceView>();
        private PlayerHandPM mPM;

        public void Init( PlayerHandPM i_pm ) {
            mPM = i_pm;
            SetModel( i_pm.ViewModel );

            CreateGamePieceViews( i_pm );
        }

        protected override void OnDestroy() {
            base.OnDestroy();

            mPM.Dispose();
        }

        private void CreateGamePieceViews( PlayerHandPM i_pm  ) {
            foreach ( GamePiecePM piecePM in i_pm.GamePiecePMs ) {
                CreateGamePieceView( piecePM );
            }
        }

        private void CreateGamePieceView( GamePiecePM i_pm ) {
            GameObject viewObject = gameObject.InstantiateUI( GamePieceViewPrefab, GamePieceViewArea );
            GamePieceView view = viewObject.GetComponent<GamePieceView>();
            view.Init( i_pm );

            mPieceViews.Add( view );
        }
    }
}
