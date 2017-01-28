
using System;
using System.Collections.Generic;

namespace Qwf.Client {
    public class ClientPlayer : IGamePlayer {
        private int mId;

        public ClientPlayer( int i_id ) {
            mId = i_id;
        }

        public int GetId() {
            return mId;
        }

        public void DrawToFillHand() {
            throw new NotImplementedException();
        }

        public List<IGamePiece> GetHeldPieces() {
            throw new NotImplementedException();
        }

        public List<IGamePiece> GetUndrawnPieces() {
            throw new NotImplementedException();
        }

        public bool IsGamePieceHeld( IGamePiece i_piece ) {
            throw new NotImplementedException();
        }

        public void RemovePieceFromHand( IGamePiece i_piece ) {
            throw new NotImplementedException();
        }
    }
}