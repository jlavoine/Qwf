
using System;
using System.Collections.Generic;

namespace Qwf.Client {
    public class ClientPlayer : IGamePlayer {
        private string mId;
        public string Id { get { return mId; } private set { mId = value; } }

        public ClientPlayer( string i_id ) {
            Id = i_id;
        }        

        public void DrawToFillHand() {
            throw new NotImplementedException();
        }

        public List<IServerGamePiece> GetHeldPieces() {
            throw new NotImplementedException();
        }

        public List<IServerGamePiece> GetUndrawnPieces() {
            throw new NotImplementedException();
        }

        public bool IsGamePieceHeld( IServerGamePiece i_piece ) {
            throw new NotImplementedException();
        }

        public void RemovePieceFromHand( IServerGamePiece i_piece ) {
            throw new NotImplementedException();
        }

        public IServerGamePiece GetHeldPieceOfIndex( int i_index ) {
            throw new NotImplementedException();
        }
    }
}