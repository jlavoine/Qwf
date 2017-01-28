using MyLibrary;
using System.Collections.Generic;

namespace Qwf.Client {
    public class PlayerHandPM : GenericViewModel {

        public List<GamePiecePM> mGamePiecePMs;
        public List<GamePiecePM> GamePiecePMs { get { return mGamePiecePMs; } }

        public PlayerHandPM( List<IGamePiece> i_gamePieces ) {
            CreateGamePiecePMs( i_gamePieces );
        }

        private void CreateGamePiecePMs( List<IGamePiece> i_gamePieces ) {
            mGamePiecePMs = new List<GamePiecePM>();
            foreach ( IGamePiece gamePiece in i_gamePieces ) {
                mGamePiecePMs.Add( new GamePiecePM( gamePiece ) );
            }
        }
    }
}
