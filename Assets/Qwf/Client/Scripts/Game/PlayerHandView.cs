using UnityEngine;
using MyLibrary;
using System.Collections.Generic;

namespace Qwf.Client {
    public class PlayerHandView : GroupView {
        public GameObject GamePieceViewArea;
        public GameObject GamePieceViewPrefab;

        private List<PlayerHandGamePieceView> mPieceViews = new List<PlayerHandGamePieceView>();
        private PlayerHandPM mPM;

        void Start() {
            PlayerHandPM pm = new PlayerHandPM( new List<IGamePieceData>(), BackendManager.Instance.GetPlayerId() );
            Init( pm );
        }

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
            foreach ( PlayerHandGamePiecePM piecePM in i_pm.GamePiecePMs ) {
                CreateGamePieceView( piecePM );
            }
        }

        private void CreateGamePieceView( PlayerHandGamePiecePM i_pm ) {
            GameObject viewObject = gameObject.InstantiateUI( GamePieceViewPrefab, GamePieceViewArea );
            PlayerHandGamePieceView view = viewObject.GetComponent<PlayerHandGamePieceView>();
            view.Init( i_pm );

            mPieceViews.Add( view );
        }
    }
}
